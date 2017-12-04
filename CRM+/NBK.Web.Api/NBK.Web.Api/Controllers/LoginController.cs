using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using NBK.Web.Api.Adapters;
using NBK.Web.Api.Models;


namespace NBK.Web.Api.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("GetToken/{username}/{userpassword}/{system}")]
        public IHttpActionResult Get(string username, string userpassword, string system)
        {


            XenonAdapter adapter = new XenonAdapter();
            Hashtable parameters = new Hashtable();
        

            string userstore = System.Configuration.ConfigurationManager.AppSettings[string.Format("{0}.UserCredentialStore", system)].ToString();
            string systemstore = System.Configuration.ConfigurationManager.AppSettings[string.Format("{0}.SystemCredentialStore", system)].ToString();
            string systemname = System.Configuration.ConfigurationManager.AppSettings[string.Format("{0}.SystemUserId", system)].ToString();
            string systempassword = System.Configuration.ConfigurationManager.AppSettings[string.Format("{0}.SystemPassword", system)].ToString();


            parameters.Add("username", username);
            parameters.Add("userpassword", userpassword);
            parameters.Add("userstore", userstore);


            parameters.Add("systemname", systemname);
            parameters.Add("systempassword", systempassword);
            parameters.Add("systemstore", systemstore);

            BaseResponse response = adapter.ExecuteCall("Login", "Amberpoint", parameters);

            if (response.ErrCode == "0")
            {
                //Deserialize json
                LoginResponse loginResponse = new LoginResponse();
                loginResponse.Token = response.Json;
                return Json(loginResponse);
            }
            else
            {
                throw new HttpRequestException(response.ErrDesc);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser/{userID}")]
        public IHttpActionResult GetUser(string userID)
        {
            NBKUser user = null;
            using (var context = new EASEntities())
            {
                string searchBy = string.Format("RACF\\{0}", userID);
                V_User_Logins loginDetails = context.V_User_Logins.Where( x => x.AssociatedAccount == searchBy).FirstOrDefault();
                if (loginDetails != null)
                {
                    user = new NBKUser();
                    user.UserID = userID;
                    user.PartyID = loginDetails.PartyID;
                    user.Name = loginDetails.NameEnglish;
                }
            }
            return Json(user);
        }
    }
}
