using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Runtime;
using System.ServiceModel;
using System.Globalization;
using System.ServiceModel.Description;
using System.Linq;
using Common;

namespace EAIP1Service
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class EAIP1Service : StatelessService
    {
        public EAIP1Service(StatelessServiceContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            yield return new ServiceInstanceListener(context =>
            {
                //return CreateRestListener(context);
                return CreateSoapListener(context);
            });
        }

        private static ICommunicationListener CreateSoapListener(StatelessServiceContext context)
        {
            string host = context.NodeContext.IPAddressOrFQDN;
            var endpointConfig = context.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
            int port = endpointConfig.Port;
            string scheme = endpointConfig.Protocol.ToString();

            string uri = string.Format(CultureInfo.InvariantCulture, "{0}://{1}:{2}/EAIP1", scheme, host, port);
            var listener = new WcfCommunicationListener<INBKCentral>(
                serviceContext: context,
                wcfServiceObject: new NBKCentral(),
                listenerBinding: new BasicHttpBinding(BasicHttpSecurityMode.None),
                address: new EndpointAddress(uri)
            );

            // Check to see if the service host already has a ServiceMetadataBehavior
            ServiceMetadataBehavior smb = listener.ServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
            // If not, add one
            if (smb == null)
            {
                smb = new ServiceMetadataBehavior();
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                smb.HttpGetEnabled = true;
                smb.HttpGetUrl = new Uri(uri);

                listener.ServiceHost.Description.Behaviors.Add(smb);
            }
            return listener;
        }

        private static ICommunicationListener CreateRestListener(StatelessServiceContext context)
        {
            string host = context.NodeContext.IPAddressOrFQDN;
            var endpointConfig = context.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
            int port = endpointConfig.Port;
            string scheme = endpointConfig.Protocol.ToString();
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}://{1}:{2}/", scheme, host, port);
            var listener = new WcfCommunicationListener<INBKCentral>(
                serviceContext: context,
                wcfServiceObject: new NBKCentral(),
                listenerBinding: new WebHttpBinding(WebHttpSecurityMode.None),
                address: new EndpointAddress(uri)
            );
            var ep = listener.ServiceHost.Description.Endpoints.Last();
            ep.Behaviors.Add(new WebHttpBehavior());
            return listener;
        }
    }
}
