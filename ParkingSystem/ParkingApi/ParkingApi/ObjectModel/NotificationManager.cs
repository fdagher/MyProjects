using ParkingApi.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ParkingApi.ObjectModel
{
    public static class NotificationManager
    {
        public static void SendNotification(string title, string body, string to, NotificationMedia media)
        {
            if (media == NotificationMedia.Email)
            {
                EmailSender.Send(ConfigurationManager.AppSettings["ParkingApi.Messaging.Email.From"], to, title, body);
            }
            else
            {
                SMSSender.Send(to, body);
            }
        }
    }

    public enum NotificationMedia
    {
        Email = 0,
        SMS = 1
    }

    public enum NotificationType
    {
        Confirmation = 0,
        Interest = 1,
        Rejection = 2,
        Success
    }
}