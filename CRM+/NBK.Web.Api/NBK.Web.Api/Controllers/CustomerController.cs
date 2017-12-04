using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using NBK.Web.Api.Adapters;
using NBK.Web.Api.Models;
using NBK.Web.Api.Helpers;

namespace NBK.Web.Api.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        /// <summary>
        /// Todo change get to Inquiry
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{customerNo}")]
        public IHttpActionResult Get(string customerNo)
        {

            //string token = Constants.SessionToken;
            string token = string.Empty;
            try
            {
                IEnumerable<string> headerValues = this.Request.Headers.GetValues("token");
                token = headerValues.FirstOrDefault();
            }
            catch
            {
                throw new HttpRequestException("Token not provided");
            }

            XenonAdapter adapter = new XenonAdapter();
            Hashtable parameters = new Hashtable();
            parameters.Add("token", token);
            parameters.Add("customerNumber", customerNo);

            DateTime startTime = DateTime.Now;
            BaseResponse response = adapter.ExecuteCall("CustomerInquiry", "BFX", parameters);

            Customer customerResponse = new Customer();

            if (response.ErrCode == "0")
            {
                //Deserialize json
                customerResponse = JsonConvert.DeserializeObject<Customer>(response.Json);
                customerResponse.CustomerNumber = customerNo;

                double ms = DateTime.Now.Subtract(startTime).TotalMilliseconds;

                return Json(customerResponse);
            }
            else
            {
                throw new HttpRequestException(response.ErrDesc);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Alerts/{customerNo}")]
        public IHttpActionResult Alerts(string customerNo)
        {
            string token = string.Empty;
            try
            {
                IEnumerable<string> headerValues = this.Request.Headers.GetValues("token");
                token = headerValues.FirstOrDefault();
            }
            catch
            {
                throw new HttpRequestException("Token not provided");
            }

            XenonAdapter adapter = new XenonAdapter();
            Hashtable parameters = new Hashtable();
            parameters.Add("token", token);
            parameters.Add("customerNumber", customerNo);

            BaseResponse response = adapter.ExecuteCall("CustomerAlert", "BFX", parameters);

            List<Alert> alertList = new List<Alert>();

            if (response.ErrCode == "0")
            {
                //Deserialize json
                alertList = JsonConvert.DeserializeObject <List<Alert>>(response.Json);
                return Json(alertList);
            }
            else
            {
                throw new HttpRequestException(response.ErrDesc);
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("FinancialProfile/{customerNo}")]
        public IHttpActionResult FinancialProfile(string customerNo)
        {
            // string token = Constants.SessionToken;
            string token = string.Empty;
            try
            {
                IEnumerable<string> headerValues = this.Request.Headers.GetValues("token");
                token = headerValues.FirstOrDefault();
            }
            catch
            {
                throw new HttpRequestException("Token not provided");
            }

            XenonAdapter adapter = new XenonAdapter();
            Hashtable parameters = new Hashtable();
            parameters.Add("token", token);
            parameters.Add("customerNumber", customerNo);

            BaseResponse response = adapter.ExecuteCall("FinancialProfile", "BFX", parameters);

            List<FinancialEntry> list = new List<FinancialEntry>();
            if (response.ErrCode == "0")
            {
                //Deserialize json
                list = JsonConvert.DeserializeObject<List<FinancialEntry>>(response.Json);

                decimal total = list.Sum(x => (decimal)x.Value);

                if (total != 0) {
                    foreach (FinancialEntry entry in list)
                    {
                        entry.Percent = ((decimal)entry.Value / total) * 100;
                    }
                }

                return Json(list);
            }
            else
            {
                throw new HttpRequestException(response.ErrDesc);
            }
        }
       



        [HttpGet]
        [Route("GetBasicInfo/{customerNo}")]
        public IHttpActionResult GetBasicInfo(string customerNo)
        {

            string token = Constants.SessionToken;
            //string token = string.Empty;
            //try
            //{
            //    IEnumerable<string> headerValues = this.Request.Headers.GetValues("token");
            //    token = headerValues.FirstOrDefault();
            //}
            //catch
            //{
            //    throw new HttpRequestException("Token not provided");
            //}

            XenonAdapter adapter = new XenonAdapter();
            Hashtable parameters = new Hashtable();
            parameters.Add("token", token);
            parameters.Add("customerNumber", customerNo);

            DateTime startTime = DateTime.Now;
            BaseResponse response = adapter.ExecuteCall("CustomerBasicInfo", "EAI", parameters);

           

            if (response.ErrCode == "0")
            {
               

                return Json("OK");
            }
            else
            {
                throw new HttpRequestException(response.ErrDesc);
            }
        }
    }
}
