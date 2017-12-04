using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    public abstract class BaseRequest : BaseModel
    {
        private TransactionHeader header;
      
        public TransactionHeader TransactionHeader
        {
            get
            {
                return this.header;
            }
            set
            {
                this.header = value;
            }
        }

        public override string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(header.Serialize());
            sb.Append(base.Serialize());
            return sb.ToString();
        }

    }
}
