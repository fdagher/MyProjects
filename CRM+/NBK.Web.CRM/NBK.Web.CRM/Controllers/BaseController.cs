using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NBK.Web.CRM.Controllers
{
    public class BaseController : Controller
    {
        public string SessionToken
        {
            get
            {
                return (string)Session["SessionToken"];
            }
            set
            {
                Session["SessionToken"] = value;
            }
        }


        public string UserID
        {
            get
            {
                return (string)Session["UserID"];
            }
            set
            {
                Session["UserID"] = value;
            }
        }
    }

}