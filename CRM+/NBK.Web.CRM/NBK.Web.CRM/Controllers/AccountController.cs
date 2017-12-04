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
using NBK.Web.CRM.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace NBK.Web.CRM.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        string _serviceURL = ConfigurationManager.AppSettings["Web.Api.Url"];
        string _servicePath = ConfigurationManager.AppSettings["Web.Api.Path"];

        // GET: /Account/Login
       // [AllowAnonymous]
       [Authorize]
        public ActionResult Login(string returnUrl)
        {
            string token = (User as ClaimsPrincipal).FindFirst("access_token").Value;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            LoginResponse loginResponse = new LoginResponse();
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Login/GetToken/{1}/{2}/CRM", _servicePath, model.UserID, model.Password);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", Constants.SessionToken);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
                    this.SessionToken = loginResponse.Token;
                    this.UserID = model.UserID;
                }
                else
                {
                    
                }
            }


            return RedirectToAction("CustomerSummary", "Customer");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetToken(string userID, string password)
        {
            LoginResponse loginResponse = new LoginResponse();
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Login/GetToken/{1}/{2}/CRM", _servicePath, userID, password);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", Constants.SessionToken);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
                    return Content(JsonConvert.SerializeObject(loginResponse));
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Content("Invalid credentials provided");
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetUser(string userID)
        {
            NBKUser user = new NBKUser();
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/api/Login/GetUser/{1}", _servicePath, userID);
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
 
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<NBKUser>();
                    return Content(JsonConvert.SerializeObject(user));
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Content("Invalid user");
                }
            }
        }
    }
}