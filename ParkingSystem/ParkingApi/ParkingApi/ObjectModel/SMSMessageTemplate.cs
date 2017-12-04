using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.ObjectModel
{
    public abstract class SMSMessageTemplate : IMessageTemplate
    {
        public abstract string Template { get; }
    }
}