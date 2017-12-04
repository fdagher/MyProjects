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
using NBK.Web.CRM.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace NBK.Web.CRM.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        string _serviceURL = ConfigurationManager.AppSettings["Web.Api.Url"];
        string _servicePath = ConfigurationManager.AppSettings["Web.Api.Path"];

        // GET: Customer

        
        public ActionResult CustomerSummary()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Update(string customerNo)
        {
            return Content("OK");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [HttpGet]
       // [SessionExpire]
        public async Task<ActionResult> Get(string customerNo)
        {
            Customer customer = new Customer();

            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Customer/Get/{1}", _servicePath, customerNo);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
                client.DefaultRequestHeaders.Add("token", this.SessionToken);

                DateTime startTime = DateTime.Now;
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    customer = await response.Content.ReadAsAsync<Customer>();

                    customer.ServiceResponseTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;

                    //Set image Demo only
                    Random r = new Random();
                    int nextNo = 1;
                    if (customer.Gender == "Male")
                    {
                        nextNo = r.Next(1, 8);
                        customer.Image = string.Format("m{0}.jpg", nextNo);
                    }
                    else
                    {
                        nextNo = r.Next(1, 8);
                        customer.Image = string.Format("f{0}.jpg", nextNo);
                    }
                   
                    return Content(JsonConvert.SerializeObject(customer));
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
        /// <returns></returns>
        [HttpGet]
        //[SessionExpire]
        public async Task<ActionResult> GetInfo(string customerNo)
        {
            string token = (User as ClaimsPrincipal).FindFirst("access_token").Value;

            string customerServiceURL = ConfigurationManager.AppSettings["CustomerService.Url"];

            Models.ServiceFabric.Customer customer = new Models.ServiceFabric.Customer();

            using (var client = new HttpClient())
            {
                string url = string.Format("services/customer/get/{0}", customerNo);
                client.BaseAddress = new Uri(customerServiceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("token", this.SessionToken);
                DateTime startTime = DateTime.Now;

                client.SetBearerToken(token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    customer = await response.Content.ReadAsAsync<Models.ServiceFabric.Customer>();
                    customer.ServiceResponseTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;

                    switch (customer.Gender)
                    {
                        case "M":
                            customer.Gender = "Male";
                            break;
                        case "F":
                            customer.Gender = "Female";
                            break;
                    }

                    //Set image Demo only
                    Random r = new Random();
                    int nextNo = 1;
                    if (customer.Gender == "Male")
                    {
                        nextNo = r.Next(1, 8);
                        customer.Image = string.Format("m{0}.jpg", nextNo);
                    }
                    else
                    {
                        nextNo = r.Next(1, 8);
                        customer.Image = string.Format("f{0}.jpg", nextNo);
                    }

                    return Content(JsonConvert.SerializeObject(customer));
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
        /// <returns></returns>
        [HttpGet]
        //[SessionExpire]
        public async Task<ActionResult> GetFinancialProfile(string customerNo)
        {
            List<FinancialEntry> list = new List<FinancialEntry>();
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Customer/FinancialProfile/{1}", _servicePath, customerNo);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", this.SessionToken);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadAsAsync<List<FinancialEntry>>();
                    return Content(JsonConvert.SerializeObject(list));
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Content(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Get Financial profile by calling service fabric
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [HttpGet]
        //[SessionExpire]
        public async Task<ActionResult> GetProfile(string customerNo)
        {
            string profileServiceURL = ConfigurationManager.AppSettings["ProfileService.Url"];

            List<FinancialEntry> list = new List<FinancialEntry>();
            Models.ServiceFabric.FinancialProfile financialProfile = new Models.ServiceFabric.FinancialProfile();

            using (var client = new HttpClient())
            {
                string url = string.Format("services/profile/{0}", customerNo);
                client.BaseAddress = new Uri(profileServiceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("token", this.SessionToken);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    financialProfile = await response.Content.ReadAsAsync<Models.ServiceFabric.FinancialProfile>();

                    if (financialProfile != null)
                    {
                        #region ****** Manually build list ******
                        FinancialEntry entry = new FinancialEntry();
                        entry.Name = "TotalCashLimit";
                        entry.Type = 1;
                        entry.Value = financialProfile.TotalCashLimit;
                        list.Add(entry);

                        entry = new FinancialEntry();
                        entry.Name = "TotalNonCashLimit";
                        entry.Type = 1;
                        entry.Value = financialProfile.TotalNonCashLimit;
                        list.Add(entry);

                        entry = new FinancialEntry();
                        entry.Name = "TotalEarnedAssets";
                        entry.Type = 1;
                        entry.Value = financialProfile.TotalEarnedAssets;
                        list.Add(entry);

                        entry = new FinancialEntry();
                        entry.Name = "TotalFreeFunds";
                        entry.Type = 1;
                        entry.Value = financialProfile.TotalFreeFunds;
                        list.Add(entry);

                        entry = new FinancialEntry();
                        entry.Name = "TotalNBKFunds";
                        entry.Type = 1;
                        entry.Value = financialProfile.TotalNBKFunds;
                        list.Add(entry);


                        entry = new FinancialEntry();
                        entry.Name = "TotalCashLiability";
                        entry.Type = 2;
                        entry.Value = financialProfile.TotalCashLiability;
                        list.Add(entry);

                        entry = new FinancialEntry();
                        entry.Name = "TotalIndirectLiability";
                        entry.Type = 2;
                        entry.Value = financialProfile.TotalIndirectLiability;
                        list.Add(entry);

                        entry = new FinancialEntry();
                        entry.Name = "TotalNonCashLiability";
                        entry.Type = 2;
                        entry.Value = financialProfile.TotalNonCashLiability;
                        list.Add(entry);

                        entry = new FinancialEntry();
                        entry.Name = "TotalCollateral";
                        entry.Type = 2;
                        entry.Value = financialProfile.TotalCollateral;
                        list.Add(entry);
                        #endregion

                        return Content(JsonConvert.SerializeObject(list));
                    }
                    else
                    {
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Content("Customer not found");
                    }
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
        /// <returns></returns>
        [HttpGet]
        [SessionExpire]
        public async Task<ActionResult> GetProducts(string customerNo)
        {
            List<Product> products = new List<Product>();
            string key = string.Format("ProductList_{0}", customerNo);

            if (Session[key] == null) {
                using (var client = new HttpClient())
                {
                    string url = string.Format("{0}/api/ProductList/Get/{1}", _servicePath, customerNo);
                    client.BaseAddress = new Uri(_serviceURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", this.SessionToken);
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        products = await response.Content.ReadAsAsync<List<Product>>();

                        string productJson = JsonConvert.SerializeObject(products.OrderBy(x => x.ProductCategory));
                        Session[key] = productJson;
                        return Content(productJson);
                    }
                    else
                    {
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Content(response.ReasonPhrase);
                    }
                }
            }
            else
            {
                return Content(Session[key].ToString());
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [HttpGet]
        [SessionExpire]
        public async Task<ActionResult> GetAlerts(string customerNo)
        {
            List<Alert> alerts = new List<Alert>();
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Customer/Alerts/{1}", _servicePath, customerNo);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", this.SessionToken);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    alerts = await response.Content.ReadAsAsync<List<Alert>>();
                    alerts = alerts.Where(x => x.Indicator == "true").ToList();
                    return Content(JsonConvert.SerializeObject(alerts));
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
        /// <returns></returns>
        [HttpGet]
        [SessionExpire]
        public async Task<ActionResult> GetCampaignLeads(string customerNo)
        {
            List<CampaignLead> leads = new List<CampaignLead>();
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/LMS/ActiveCampaignLeads/{1}", _servicePath, customerNo);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    leads = await response.Content.ReadAsAsync<List<CampaignLead>>();
                    return Content(JsonConvert.SerializeObject(leads));
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
        /// <param name="accountNo"></param>
        /// <returns></returns>
        [HttpGet]
        [SessionExpire]
        public async Task<ActionResult> AccountDetails(string accountNo)
        {
            Account account = new Account();

            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/ProductList/AccountDetails/{1}", _servicePath, accountNo);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("token", this.SessionToken);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    account = await response.Content.ReadAsAsync<Account>();

                    return Content(JsonConvert.SerializeObject(account));
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