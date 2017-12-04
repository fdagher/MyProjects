using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    public class Account : BaseModel
    {
        private string number;
        private string shortname;
        private string type;
        private string description;
        private string currency;
        private string status;
        private Decimal currentbalance;
        private string currentsign;
        private Decimal ledgerbalance;
        private string ledgersign;
        private Decimal availablebalance;
        private string availablesign;
        private Decimal convertedbalance;
        private string convertedsign;

        [Position(0, 13)]
        public string Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
            }
        }

        [Position(13, 15)]
        public string ShortName
        {
            get
            {
                return this.shortname;
            }
            set
            {
                this.shortname = value;
            }
        }

        [Position(28, 2)]
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        [Position(30, 3)]
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

        [Position(33, 1)]
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        [Position(34, 1)]
        public string CurrentBalanceSign
        {
            get
            {
                return this.currentsign;
            }
            set
            {
                this.currentsign = value;
            }
        }

        [Position(35, 15)]
        public Decimal CurrentBalance
        {
            get
            {
                return this.currentbalance;
            }
            set
            {
                this.currentbalance = value;
            }
        }

        [Position(50, 1)]
        public string LedgerBalanceSign
        {
            get
            {
                return this.ledgersign;
            }
            set
            {
                this.ledgersign = value;
            }
        }

        [Position(51, 15)]
        public Decimal LedgerBalance
        {
            get
            {
                return this.ledgerbalance;
            }
            set
            {
                this.ledgerbalance = value;
            }
        }

        [Position(66, 1)]
        public string AvailableBalanceSign
        {
            get
            {
                return this.availablesign;
            }
            set
            {
                this.availablesign = value;
            }
        }

        [Position(67, 15)]
        public Decimal AvailableBalance
        {
            get
            {
                return this.availablebalance;
            }
            set
            {
                this.availablebalance = value;
            }
        }

        [Position(82, 1)]
        public string ConvertedBalanceSign
        {
            get
            {
                return this.convertedsign;
            }
            set
            {
                this.convertedsign = value;
            }
        }

        [Position(83, 15)]
        public Decimal ConvertedBalance
        {
            get
            {
                return this.convertedbalance;
            }
            set
            {
                this.convertedbalance = value;
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
