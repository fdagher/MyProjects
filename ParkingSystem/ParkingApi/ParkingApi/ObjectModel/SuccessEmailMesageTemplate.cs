using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.ObjectModel
{
    public class SuccessEmailMesageTemplate : EmailMessageTemplate
    {
        private User user;

        public SuccessEmailMesageTemplate(User user)
        {
            this.user = user;
        }

        public override string Template
        {
            get
            {
                return string.Format("Congratulations {0}! Your parking slot '{1}' has been confirmed for today ({2}).", 
                    this.user.Name, this.user.ParkingSlot, this.user.Date.ToLongDateString());
            }
        }
    }
}