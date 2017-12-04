using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBK.Web.Api.Models.Equation;
using NBK.Web.Api.Adapters.MQ;



namespace NBK.Web.Api.Adapters
{
    public class EquationAdapter
    {
        HostType _adapter;

        public EquationAdapter()
        {
            _adapter = HostType.Equation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Customer CustomerInquiry(CustomerRequest request)
        {
            Customer response = new Customer();
            string requestMessage = request.Serialize();
            MQAdapter mqAdapter = new MQAdapter(_adapter);
            string responseMessage = mqAdapter.SendMQRequestSync(requestMessage);
            response.DeSerialize(responseMessage);
            return response;
        }

        public PortfolioResponse PortfolioInquiry(PortfolioRequest request)
        {
            PortfolioResponse response = new PortfolioResponse();
            string requestMessage = request.Serialize();
            MQAdapter mqAdapter = new MQAdapter(_adapter);
            string responseMessage = mqAdapter.SendMQRequestSync(requestMessage);
            response.DeSerialize(responseMessage);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TransactionHistoryResponse TransactionHistory(TransactionHistoryRequest request)
        {
            TransactionHistoryResponse response = new TransactionHistoryResponse();
            string requestMessage = request.Serialize();
            MQAdapter mqAdapter = new MQAdapter(_adapter);
            string responseMessage = mqAdapter.SendMQRequestSync(requestMessage);
            response.DeSerialize(responseMessage);
            return response;
        }
    }
}
