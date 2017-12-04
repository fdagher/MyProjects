using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    public class Transaction : BaseModel
    {
        private int seqno;
        private string postingDate;
        private string code;
        private string drCrFlag;
        private decimal amount;
        private string amountStr;
        private string runningBalanceSign;
        private decimal runningBalance;
        private string runningBalanceStr;
        private string userReference;
        private string narrativeLine1;
        private string narrativeLine2;
        private string narrativeLine3;
        private string narrativeLine4;
        private string narrativeLines;
        private string valueDate;
        private string description;


        public int Seqno
        {
            get
            {
                return this.seqno;
            }
            set
            {
                this.seqno = value;
            }
        }

        [Position(0, 6)]
        public string PostingDate
        {
            get 
            {
                return this.postingDate;
            }
            set 
            {
                this.postingDate = value;
            }
        }

   
        [Position(6, 3)]
        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        [Position(9, 1)]
        public string DrCrFlag
        {
            get
            {
                return this.drCrFlag;
            }
            set
            {
                this.drCrFlag = value;
            }
        }

        [Position(10, 15)]
        public decimal Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                this.amount = value;
            }
        }

        public string AmountStr
        {
            get
            {
                return String.Format("{0:0.000}", this.amount); ;
            }
            set
            {
                this.amountStr = value;
            }
        }

        [Position(25, 1)]
        public string RunningBalanceSign
        {
            get
            {
                return this.runningBalanceSign;
            }
            set
            {
                this.runningBalanceSign = value;
            }
        }

        [Position(26, 15)]
        public decimal RunningBalance
        {
            get
            {
                return this.runningBalance;
            }
            set
            {
                this.runningBalance = value;
            }
        }

        public string RunningBalanceStr
        {
            get
            {
                string balance = String.Format("{0:0.000}", this.runningBalance);
                if (this.DrCrFlag == "D")
                {
                    return "-" + balance;
                }
                else
                {
                    return balance;
                }
            }
            set
            {
                this.runningBalanceStr = value;
            }
        }

        [Position(41, 16)]
        public string UserReference
        {
            get
            {
                return this.userReference;
            }
            set
            {
                this.userReference = value;
            }
        }

        [Position(57, 35)]
        public string NarrativeLine1
        {
            get
            {
                return this.narrativeLine1;
            }
            set
            {
                this.narrativeLine1 = value;
            }
        }

        [Position(92, 35)]
        public string NarrativeLine2
        {
            get
            {
                return this.narrativeLine2;
            }
            set
            {
                this.narrativeLine2 = value;
            }
        }

        [Position(127, 35)]
        public string NarrativeLine3
        {
            get
            {
                return this.narrativeLine3;
            }
            set
            {
                this.narrativeLine3 = value;
            }
        }

        [Position(162, 35)]
        public string NarrativeLine4
        {
            get
            {
                return this.narrativeLine4;
            }
            set
            {
                this.narrativeLine4 = value;
            }
        }

        public string NarrativeLines
        {
            get
            {
                return this.narrativeLine1 + "\r\n" + this.narrativeLine2 + "\r\n" + this.narrativeLine3 + "\r\n" + this.narrativeLine4;
            }
            set
            {
                this.narrativeLines = value;
            }
        }

        [Position(197, 6)]
        public string ValueDate
        {
            get
            {
                return this.valueDate;
            }
            set
            {
                this.valueDate = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
    }
}
