using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBK.Web.Api.Models
{

    public abstract class BaseRequest
    {
        private string token;
        private string transaction;


        public string Token
        {
            get
            {
                return this.token;
            }
            set
            {
                this.token = value;
            }
        }


        public string Transaction
        {
            get
            {
                return this.transaction;
            }
            set
            {
                this.transaction = value;
            }
        }


    }
}