using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    public class PortfolioResponse : BaseResponse
    {
        private string responseCode;
        private string responseDescription;
        private string customerno;
        private string customername;
        private int noofaccounts;
        private List<Account> accountlist;

        [Position(300, 7)]
        public string ResponseCode
        {
            get
            {
                return this.responseCode;
            }
            set
            {
                this.responseCode = value;
            }
        }

        [Position(307, 50)]
        public string ResponseDescription
        {
            get
            {
                return this.responseDescription;
            }
            set
            {
                this.responseDescription = value;
            }
        }

        [Position(357, 6)]
        public string CustomerNo
        {
            get
            {
                return this.customerno;
            }
            set
            {
                this.customerno = value;
            }
        }

        [Position(363, 35)]
        public string CustomerName
        {
            get
            {
                return this.customername;
            }
            set
            {
                this.customername = value;
            }
        }

        [Position(398, 5)]
        public int NoOfAccounts
        {
            get
            {
                return this.noofaccounts;
            }
            set
            {
                this.noofaccounts = value;
            }
        }

       
        [RepeatData(403, 98)]
        [RepeatCount(398, 5)]
        public List<Account> Accounts
        {
            get
            {
                return this.accountlist;
            }
            set
            {
                this.accountlist = value;
            }
        }

        public double ResponseTime { get; set; }

        public IQueryable<Account> IQAccounts
        {
            get
            {
                return this.accountlist.AsQueryable();
            }

        }
    }
}
