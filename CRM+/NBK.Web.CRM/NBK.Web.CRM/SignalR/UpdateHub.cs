using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace NBK.Web.CRM.Hubs
{
    public class UpdateHub : Hub
    {
        public void Send(string type, string customerNo, string userID)
        {
            Clients.All.broadcastMessage(type, customerNo, userID);
        }
    }
}