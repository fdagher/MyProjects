using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace NBK.Web.Api.Models
{
    public class CustomerPortfolioResponse
    {
        public string ErrCode { get; set; }

        public string ErrDesc { get; set; }
        //public List<Product> Products { get; set; }
    }
}