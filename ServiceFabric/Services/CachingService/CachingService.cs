using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Fabric.Description;
using System.Net;
using System.Text;
using Microsoft.ServiceFabric.Data;

namespace CachingService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CachingService : StatefulService
    {
        public CachingService(StatefulServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[] { new ServiceReplicaListener(context => this.CreateInternalListener(context)) };
        }

        private ICommunicationListener CreateInternalListener(ServiceContext context)
        {
            // Partition replica's URL is the node's IP, port, PartitionId, ReplicaId, Guid
            EndpointResourceDescription internalEndpoint = context.CodePackageActivationContext.GetEndpoint("CachingServiceEndpoint");

            // Multiple replicas of this service may be hosted on the same machine,
            // so this address needs to be unique to the replica which is why we have partition ID + replica ID in the URL.
            // HttpListener can listen on multiple addresses on the same port as long as the URL prefix is unique.
            // The extra GUID is there for an advanced case where secondary replicas also listen for read-only requests.
            // When that's the case, we want to make sure that a new unique address is used when transitioning from primary to secondary
            // to force clients to re-resolve the address.
            // '+' is used as the address here so that the replica listens on all available hosts (IP, FQDM, localhost, etc.)

            //string uriPrefix = String.Format(
            //    "{0}://+:{1}/{2}/{3}-{4}/",
            //    internalEndpoint.Protocol,
            //    internalEndpoint.Port,
            //    context.PartitionId,
            //    context.ReplicaOrInstanceId,
            //    Guid.NewGuid());

            string uriPrefix = String.Format(
                "{0}://+:{1}/services/cachingservice/",
                internalEndpoint.Protocol,
                internalEndpoint.Port);

            string nodeIP = FabricRuntime.GetNodeContext().IPAddressOrFQDN;

            // The published URL is slightly different from the listening URL prefix.
            // The listening URL is given to HttpListener.
            // The published URL is the URL that is published to the Service Fabric Naming Service,
            // which is used for service discovery. Clients will ask for this address through that discovery service.
            // The address that clients get needs to have the actual IP or FQDN of the node in order to connect,
            // so we need to replace '+' with the node's IP or FQDN.
            string uriPublished = uriPrefix.Replace("+", nodeIP);
            return new HttpCommunicationListener(uriPrefix, uriPublished, this.ProcessInternalRequest);
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        /*protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
                    // discarded, and nothing is saved to the secondary replicas.
                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
        */

        private async Task ProcessInternalRequest(HttpListenerContext context, CancellationToken cancelRequest)
        {
            string output = null;
            string method = context.Request.HttpMethod;
            string collection = context.Request.QueryString["collection"].ToString();
            string key = context.Request.QueryString["key"].ToString();

            if (!string.IsNullOrEmpty(collection) && !string.IsNullOrEmpty(key))
            {
                try
                {
                    if (method.ToUpperInvariant() == "GET")
                    {
                        output = await this.GetItemToCacheAsync(collection, key);
                    }
                    else if (method.ToUpperInvariant() == "POST")
                    {
                        string data = this.GetRequestPostData(context.Request);

                        output = await this.AddItemToCacheAsync(collection, key, data);
                    }
                    else if (method.ToUpperInvariant() == "DELETE")
                    {
                        output = await this.DeleteItemFromCacheAsync(collection, key);
                    }
                }
                catch (Exception ex)
                {
                    output = ex.Message;
                }
            }
            
            using (HttpListenerResponse response = context.Response)
            {
                if (output != null)
                {
                    byte[] outBytes = Encoding.UTF8.GetBytes(output);
                    response.OutputStream.Write(outBytes, 0, outBytes.Length);
                }
            }
        }

        public string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private async Task<string> AddItemToCacheAsync(string collection, string key, string data)
        {
            IReliableDictionary<String, String> dictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<String, String>>(collection);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                bool addResult = await dictionary.TryAddAsync(tx, key.ToUpperInvariant(), data);

                await tx.CommitAsync();

                return String.Format(
                    "Type '{0}', Item '{1}' {2}",
                    collection,
                    key,
                    addResult ? "sucessfully added" : "already exists");
            }
        }

        private async Task<string> GetItemToCacheAsync(string collection, string key)
        {
            string data = null;

            IReliableDictionary<String, String> dictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<String, String>>(collection);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                ConditionalValue<string> getResult = await dictionary.TryGetValueAsync(tx, key.ToUpperInvariant());

                if (getResult.HasValue)
                {
                    data = getResult.Value;
                }
                await tx.CommitAsync();

                return string.IsNullOrEmpty(data) ? "Not Found." : data;
            }
        }

        private async Task<string> DeleteItemFromCacheAsync(string collection, string key)
        {
            string data = null;

            IReliableDictionary<String, String> dictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<String, String>>(collection);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                ConditionalValue<string> getResult = await dictionary.TryRemoveAsync(tx, key.ToUpperInvariant());

                data = String.Format("Type '{0}', Item '{1}' {2}", 
                                    collection, 
                                    key,
                                    getResult.HasValue ? "sucessfully deleted" : "not deleted.");

                await tx.CommitAsync();

                return data;
            }
        }
    }
}
