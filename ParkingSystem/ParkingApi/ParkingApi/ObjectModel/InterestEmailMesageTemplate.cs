using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.ObjectModel
{
    public class InterestEmailMesageTemplate : EmailMessageTemplate
    {
        private User user;
        private User targetUser;
        private string baseurl;

        public InterestEmailMesageTemplate(User user, User targetUser, string baseUrl)
        {
            this.user = user;
            this.targetUser = targetUser;
            this.baseurl = baseUrl;
        }

        public override string Template
        {
            get
            {
                return string.Format(
                    "Dear {0}, <BR/><BR/>" +
                    "Your colleague '{1}' has cancelled his slot '{2}' for today's ({3}) parking. " +
                    "If interested, please click on below link to take his spot.<BR/><BR/>" +
                    "<a href='{4}/" + targetUser.Id + "/confirm/" + user.ParkingSlot + "'>Confirm Slot</a><BR/><BR/>",
                    this.targetUser.Name, this.user.Name, this.user.ParkingSlot, this.user.Date.ToLongDateString(), this.baseurl);
            }
        }
    }
}