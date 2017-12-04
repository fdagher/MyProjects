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
using NBK.Web.CRM.Models;
using Newtonsoft.Json;

namespace NBK.Web.CRM.Controllers
{
    public class AccessLogController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult List(string userID, int count)
        {
            List<CustomerAccessLog> list = new List<CustomerAccessLog>();
            using (var context = new CRMEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                list = context.CustomerAccessLogs.Where(x => x.UserID == userID).OrderByDescending(x => x.DateTime).Take(count).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessLog"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(CustomerAccessLog accessLog)
        {
            using (var context = new CRMEntities())
            {
                accessLog.DateTime = DateTime.Now;
                accessLog.ProfileType = "Normal";

                CustomerAccessLog _accessLog = context.CustomerAccessLogs.Where(
                    x => x.CustomerNo == accessLog.CustomerNo && x.UserID == accessLog.UserID).FirstOrDefault();

                if (_accessLog == null)
                {
                    context.CustomerAccessLogs.Add(accessLog);
                }
                else
                {
                    context.Entry(_accessLog).CurrentValues.SetValues(accessLog);
                }
               
                context.SaveChanges();
            }
            return Content("OK");
        }
    }
}