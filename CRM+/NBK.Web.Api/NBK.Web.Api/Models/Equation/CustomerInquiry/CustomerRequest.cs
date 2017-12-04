using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    [MessageDefinition(100)]
    public class CustomerRequest : BaseRequest
    {
       
        private string customerNo;

      
        [Position(53, 6)]
        public string CustomerNo
        {
            get
            {
                return this.customerNo;
            }
            set
            {
                this.customerNo = value;
            }
        }


        public override string Serialize()
        {
            base.TransactionHeader.MessageType = "CUST_DET_ENQ";
            return base.Serialize();
        }
    }
}
