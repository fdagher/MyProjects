using System;
using System.Collections.Generic;
using System.Linq;

namespace NBK.Web.Api.Models
{
   

    public class FinancialEntry
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Value { get; set; }
        public decimal Percent { get; set; }
    }

 
}