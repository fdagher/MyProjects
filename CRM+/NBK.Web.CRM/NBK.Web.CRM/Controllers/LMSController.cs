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
using NBK.Web.CRM.Models.LMS;
using Newtonsoft.Json;

namespace NBK.Web.CRM.Controllers
{
    public class LMSController : BaseController
    {
        string _serviceURL = ConfigurationManager.AppSettings["Web.Api.Url"];
        string _servicePath = ConfigurationManager.AppSettings["Web.Api.Path"];

        // GET: LMS
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Monitor()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetLeadsMonitor(string userID)
        {
            List<InstantLead> leads = new List<InstantLead>();
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/LMS/LeadMonitor/{1}", _servicePath, userID);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    leads = await response.Content.ReadAsAsync<List<InstantLead>>();
                    return Content(JsonConvert.SerializeObject(leads.OrderByDescending(x => x.CreateDate)));
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Content(response.ReasonPhrase);
                }
            }
        }
    }
}