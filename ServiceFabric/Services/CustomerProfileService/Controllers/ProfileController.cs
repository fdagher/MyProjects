using Common;
using CustomerProfileService.Communication;
using CustomerProfileService.DTOs;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Fabric;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.Http;

namespace CustomerProfileService.Controllers
{
    [ServiceRequestActionFilter]
    public class ProfileController : ApiController
    {
        private static readonly Uri eaip1ServiceUri;

        static ProfileController()
        {
            eaip1ServiceUri = new Uri(FabricRuntime.GetActivationContext().ApplicationName + "/EAIP1Service");
        }

        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public CustomerProfile GetById(string id)
        {
            CustomerProfile _profile = null;

            Binding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);

            // Create a partition resolver
            IServicePartitionResolver partitionResolver = ServicePartitionResolver.GetDefault();
            // create a  WcfCommunicationClientFactory object.
            var wcfClientFactory = new WcfCommunicationClientFactory<INBKCentral>
                (clientBinding: binding, servicePartitionResolver: partitionResolver);


            //
            // Create a client for communicating with the ICalculator service that has been created with the
            // Singleton partition scheme.
            //
            var nbkcentralServiceCommunicationClient = new EAIP1CommunicationClient(
                            wcfClientFactory,
                            eaip1ServiceUri,
                            ServicePartitionKey.Singleton);

            int _total = 0;
            //
            // Call the service to perform the operation.
            //
            DataSet _result = nbkcentralServiceCommunicationClient.InvokeWithRetry(
                            client => client.Channel.Read("Customer_Process_Portfolio", 
                            "GetCustomerProfile",
                            new NBK.Common.Foundations.Utilities.FilterCriteria[] { new NBK.Common.Foundations.Utilities.FilterCriteria { CompOp = NBK.Common.Foundations.Utilities.ComparisonOperator.Equal, CondOp = NBK.Common.Foundations.Utilities.ConditionalOperator.And, FieldName = "CustomerNo", FieldValue = new string[] { id }, SubExpr = NBK.Common.Foundations.Utilities.SubExpression.None } }, null, -1, -1, ref _total));

            if (_result != null && _result.Tables.Count > 0 && _result.Tables[0].Rows.Count > 0)
            {
                DataRow _row = _result.Tables[0].Rows[0];

                _profile = new CustomerProfile
                {
                    CustomerNumber = _row["CustomerNumber"].ToString(),
                    TotalCashLiability = _row["TotalCashLiability"].ToString(),
                    TotalCashLimit = _row["TotalCashLimit"].ToString(),
                    TotalCollateral = _row["TotalCollateral"].ToString(),
                    TotalEarnedAssets = _row["TotalEarnedAssets"].ToString(),
                    TotalFreeFunds = _row["TotalFreeFunds"].ToString(),
                    TotalIndirectLiability = _row["TotalIndirectLiability"].ToString(),
                    TotalNBKFunds = _row["TotalNBKFundValue"].ToString(),
                    TotalNonCashLiability = _row["TotalNonCashLiability"].ToString(),
                    TotalNonCashLimit = _row["TotalNonCashLimit"].ToString()
                };
            }

            return _profile;
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
