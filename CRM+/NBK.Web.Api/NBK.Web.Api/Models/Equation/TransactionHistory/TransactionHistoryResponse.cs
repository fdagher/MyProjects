using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    public class TransactionHistoryResponse : BaseResponse
    {
        private string responseCode;
        private string responseDescription;
        private string accountNumber;
        private string fromDate;
        private string toDate;
        private int noOfTransactions;
        private List<Transaction> transactionList;

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

        [Position(357, 13)]
        public string AccountNumber
        {
            get 
            {
                return this.accountNumber;
            }
            set
            {
                this.accountNumber = value;
            }
        }

        [Position(370, 6)]
        public string FromDate
        {
            get
            {
                return this.fromDate;
            }
            set
            {
                this.fromDate = value;
            }
        }

        [Position(376, 6)]
        public string ToDate
        {
            get
            {
                return this.toDate;
            }
            set
            {
                this.toDate = value;
            }
        }

        [Position(382, 5)]
        public int NoOfTransactions
        {
            get
            {
                return this.noOfTransactions;
            }
            set
            {
                this.noOfTransactions = value;
            }
        }

        [RepeatData(387, 203)]
        [RepeatCount(382, 5)]
        public List<Transaction> Transactions
        {
            get
            {
                return this.transactionList;
            }
            set
            {
                this.transactionList = value;
            }
        }

        public IQueryable<Transaction> IQTransactions
        {
            get
            {
                return this.transactionList.AsQueryable(); ;
            }
        }
    }
}
