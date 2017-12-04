using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    [MessageDefinition(100)]
    public class TransactionHistoryRequest : BaseRequest
    {
        private string currency;
        private string accountBranch;
        private string accountBasicNo;
        private string accountSuffix;
        private string startDate;
        private string endDate;
        private string marketSegment;
        private string servicePackage;

        [Position(0, 4)]
        public string AccountBranch
        {
            get
            {
                return this.accountBranch;
            }
            set
            {
                this.accountBranch = value;
            }
        }

        [Position(4, 6)]
        public string AccountBasicNo
        {
            get
            {
                return this.accountBasicNo;
            }
            set
            {
                this.accountBasicNo = value;
            }
        }

        [Position(10, 3)]
        public string AccountSuffix
        {
            get
            {
                return this.accountSuffix;
            }
            set
            {
                this.accountSuffix = value;
            }
        }

        [Position(32, 6)]
        public string StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                this.startDate = value;
            }
        }

        [Position(38, 6)]
        public string EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }

        [Position(59, 8)]
        public string MarketSegment
        {
            get
            {
                return this.marketSegment;
            }
            set
            {
                this.marketSegment = value;
            }
        }

        [Position(67, 8)]
        public string ServicePackage
        {
            get
            {
                return this.servicePackage;
            }
            set
            {
                this.servicePackage = value;
            }
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Serialize()
        {
            base.TransactionHeader.MessageType = "TRANS_HIST";
            return base.Serialize();
        }
    }
}
