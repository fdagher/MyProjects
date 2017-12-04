using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    [MessageDefinition(100)]
    public class PortfolioRequest : BaseRequest
    {
       
        private string customerNo;
        private string currency;
        private string marketsegment;
        private string package;

        [Position(26, 3)]
        public string Currency
        {
            get
            {
                return this.currency;
            }
            set
            {
                this.currency = value;
            }
        }
 
        [Position(53, 6)]
        public string CustomerNo
        {
            get
            {
                return this.customerNo;
            }
            set
            {
                this.customerNo = value;
            }
        }

        [Position(59, 8)]
        public string MarketSegment
        {
            get
            {
                return this.marketsegment;
            }
            set
            {
                this.marketsegment = value;
            }
        }

        [Position(67, 8)]
        public string ServicePackage
        {
            get
            {
                return this.package;
            }
            set
            {
                this.package = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Serialize()
        {
            base.TransactionHeader.MessageType = "ACC_SUMM";
            return base.Serialize();
        }
    }
}
