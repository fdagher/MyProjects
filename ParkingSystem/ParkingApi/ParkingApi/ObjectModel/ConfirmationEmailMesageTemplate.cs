using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.ObjectModel
{
    public class ConfirmationEmailMesageTemplate : EmailMessageTemplate
    {
        private User user;
        private IQueryable<User> unslottedUsers;
        private string baseurl;

        public ConfirmationEmailMesageTemplate(User user, IQueryable<User> unslottedUsers, string baseUrl)
        {
            this.user = user;
            this.unslottedUsers = unslottedUsers;
            this.baseurl = baseUrl;
        }

        public override string Template
        {
            get
            {
                return string.Format(
                    "Dear {0}, <BR/><BR/>" +
                    "Please select your decision on the parking slot '{1}' assigned to you for today ({2}).<BR/><BR/>" +
                    "<a href='{3}/" + user.Id + "/confirm/" + user.ParkingSlot + "'>Confirm Slot</a><BR/><BR/>" +
                    "<a href='{3}/" + user.Id + "/cancel'>Cancel Slot</a><BR/><BR/>" +
                    "Forward your slot to one of the below colleagues:<BR/><BR/>" +
                    GetUsersTemplate(), 
                    this.user.Name, this.user.ParkingSlot, this.user.Date.ToLongDateString(), this.baseurl);
            }
        }

        private string GetUsersTemplate()
        {
            string result = "";

            foreach (User usr in this.unslottedUsers)
            {
                result += "<a href='" + this.baseurl + "/" + user.Id + "/forward/" + usr.Id + "'>" + usr.Name + "</a><BR/><BR/>";
            }

            return result;
        }
    }
}