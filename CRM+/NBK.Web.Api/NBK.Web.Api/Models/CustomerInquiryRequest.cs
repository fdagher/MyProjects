using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBK.Web.Api.Models
{
    public class CustomerInquiryRequest : BaseRequest
    {

        public string CustomerNumber { get; set; }
    
        public new string Transaction
        {
            get
            {
                return "CustomerInquiry";
            }
        }
    }
}