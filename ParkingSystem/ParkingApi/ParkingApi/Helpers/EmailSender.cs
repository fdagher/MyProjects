using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Configuration;

namespace ParkingApi.Helpers
{
    public static class EmailSender
    {
        public static bool Send(string from, string to, string title, string body)
        {
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = title;
            message.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["ParkingApi.Mail.SMTPServerAddress"];
            smtp.UseDefaultCredentials = true;
            
            //smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["ParkingApi.Mail.SMTPLogonID"],
            //                                                    ConfigurationManager.AppSettings["ParkingApi.Mail.SMTPLogonPassword"]);

            //smtp.EnableSsl = true;
            smtp.Send(message);

            return true;
        }
    }
}