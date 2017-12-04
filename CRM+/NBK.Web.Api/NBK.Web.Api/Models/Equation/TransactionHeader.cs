using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NBK.Web.Api.Models.Equation
{
    [MessageDefinition(300)]
    public class TransactionHeader : BaseModel
    {
        private string lengthind;
        private string messagetype;
        private string channel;
        private string tranreference;
        private string userid;
        private string trandate;
        private string trantime;
        private string trandatereply;
        private string trantimereply;
        private string country;
        private string environment;

        private TransactionHeader()
        {
        }
        /// <summary>
        /// Transaction Header Constructor
        /// </summary>
        /// <param name="country"></param>
        /// <param name="environment"></param>
        /// <param name="sequence"></param>
        public TransactionHeader(string country, string environment, string userID, int sequence)
        {
            DateTime dt = DateTime.Now;
            this.country = country;
            this.environment = environment;
            this.channel = "INTBNK";
            string date = dt.Year.ToString().Substring(2) + dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0');
            this.trandate = date;
            this.trantime = dt.Hour.ToString().PadLeft(2, '0') + dt.Minute.ToString().PadLeft(2, '0') + dt.Second.ToString().PadLeft(2, '0');
            this.tranreference = country + date + sequence.ToString().PadLeft(8, '0');
        }

        [Position(0, 8)]
        public string LengthIndicator
        {
            get
            {
                return this.lengthind;
            }
            set
            {
                this.lengthind = value;
            }
        }

        [Position(8, 50)]
        public string MessageType
        {
            get
            {
                return this.messagetype;
            }
            set
            {
                this.messagetype = value;
            }
        }

        [Position(58, 16)]
        public string TranReference
        {
            get
            {
                return this.tranreference;
            }
            set
            {
                this.tranreference = value;
            }
        }

        [Position(74, 10)]
        public string Channel
        {
            get
            {
                return this.channel;
            }
            set
            {
                this.channel = value;
            }
        }

        [Position(98, 10)]
        public string UserID
        {
            get
            {
                return this.userid;
            }
            set
            {
                this.userid = value;
            }
        }

        [Position(118, 6)]
        public string TranDate
        {
            get
            {
                return this.trandate;
            }
            set
            {
                this.trandate = value;
            }
        }

        [Position(124, 6)]
        public string TranTime
        {
            get
            {
                return this.trantime;
            }
            set
            {
                this.trantime = value;
            }
        }

        [Position(130, 6)]
        public string TranDateReply
        {
            get
            {
                return this.trandatereply;
            }
            set
            {
                this.trandatereply = value;
            }
        }

        [Position(136, 6)]
        public string TranTimeReply
        {
            get
            {
                return this.trantimereply;
            }
            set
            {
                this.trantimereply = value;
            }
        }

        [Position(142, 2)]
        public string Country
        {
            get
            {
                return this.country;
            }
            set
            {
                this.country = value;
            }
        }

        [Position(144, 3)]
        public string HostEnvironment
        {
            get
            {
                return this.environment;
            }
            set
            {
                this.environment = value;
            }
        }

    }
       
}
