using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBK.Web.Api.Models
{
    public class Account
    {
        public string AccountNo { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }

        public string StatementCycle { get; set; }
        public string StatementCycleCode { get; set; }

        public string Language { get; set; }
        public string StopIndicator { get; set; }
        public Nullable<DateTime> OpenDate { get; set; }

        public Nullable<decimal> CurrentBalance { get; set; }
        public Nullable<decimal> AvailableBalance { get; set; }
        public Nullable<decimal> EffectiveBalance { get; set; }
        public Nullable<decimal> HoldAmount { get; set; }
    }
}
