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
using Newtonsoft.Json;

namespace NBK.Web.CRM.Controllers
{
    public class EquationController : Controller
    {
        string _serviceURL = ConfigurationManager.AppSettings["Web.Api.Url"];
        string _servicePath = ConfigurationManager.AppSettings["Web.Api.Path"];

        // GET: Equation
        public ActionResult IBGSummary()
        {
            return View();
        }


        [HttpGet]
        //[SessionExpire]
        public async Task<ActionResult> CustomerInquiry(string customerNo, string country)
        {
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Equation/CustomerInquiry/{1}/{2}", _servicePath, customerNo, country);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           
                DateTime startTime = DateTime.Now;
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();


                    return Content(json);
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Content(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <param name="country"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        [HttpGet]
        //[SessionExpire]
        public async Task<ActionResult> PortfolioInquiry(string customerNo, string country, string currency)
        {
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Equation/PortfolioInquiry/{1}/{2}/{3}", _servicePath, customerNo, country, currency);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                DateTime startTime = DateTime.Now;
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();


                    return Content(json);
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Content(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <param name="country"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        [HttpGet]
        //[SessionExpire]
        public async Task<ActionResult> TransactionHistory(string accountNo, string country, string currency)
        {
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Equation/TransactionHistory/{1}/{2}/{3}", _servicePath, accountNo, country, currency);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                DateTime startTime = DateTime.Now;
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return Content(json);
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