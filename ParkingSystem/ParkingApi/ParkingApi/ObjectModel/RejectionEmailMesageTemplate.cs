using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.ObjectModel
{
    public class RejectionEmailMesageTemplate : EmailMessageTemplate
    {
        private User user;

        public RejectionEmailMesageTemplate(User user)
        {
            this.user = user;
        }

        public override string Template
        {
            get
            {
                return string.Format("We are sorry {0}! The parking slot '{1}' you were trying to book for today ({2}) was taken by somebody else.", 
                    this.user.Name, this.user.ParkingSlot, this.user.Date.ToLongDateString());
            }
        }
    }
}