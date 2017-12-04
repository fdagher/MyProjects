using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using NBK.Web.Api.Models;
using NBK.Web.CRM.Models.LMS;
using NBK.Web.CRM.Helpers;
using Newtonsoft.Json;

namespace NBK.Web.CRM.Controllers
{
    public class DashboardsController : Controller
    {
        string _serviceURL = ConfigurationManager.AppSettings["Web.Api.Url"];
        string _servicePath = ConfigurationManager.AppSettings["Web.Api.Path"];

        public ActionResult Dashboard_1()
        {
            return View();
        }

        public ActionResult Dashboard_2()
        {
            return View();
        }

        public ActionResult Dashboard_3()
        {
            return View();
        }
        
        public ActionResult Dashboard_4()
        {
            return View();
        }
        
        public ActionResult Dashboard_4_1()
        {
            return View();
        }

        public ActionResult Dashboard_5()
        {
            return View();
        }

    


    }
}