using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBK.Web.Api.Models
{
    public class BaseResponse
    {
        public string ErrCode { get; set; }

        public string ErrDesc { get; set; }
       

        public string SoapMessage { get; set; }
        public string Json { get; set; }

        public string Transaction { get; set; }
       
        
    }
}