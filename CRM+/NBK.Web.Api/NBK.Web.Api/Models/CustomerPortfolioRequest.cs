using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBK.Web.Api.Models
{
    public class CustomerPortfolioRequest : BaseRequest
    {
      
        public string CustomerNumber { get; set; }
        public string ProductStatus { get; set; }
        public string ProductListType { get; set; }
        public string InquiryMode { get; set; }
        public string CustLinkMode { get; set;}
       
        public new string Transaction
        {
            get
            {
                return "CustomerPortfolio";
            }
        }

      
      
    }
}