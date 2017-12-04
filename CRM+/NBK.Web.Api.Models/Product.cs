using System;
using System.Collections.Generic;
using System.Linq;

namespace NBK.Web.Api.Models
{

    public class Product
    {
        public string CustomerNumber { get; set; }
        public string AccountStatus { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string TransferStatus { get; set; }
        public string CurrencyCode { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string Code { get; set; }
        public string NumberExt { get; set; }

    }


}
