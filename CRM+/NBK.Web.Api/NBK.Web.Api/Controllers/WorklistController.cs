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
    [RoutePrefix("api/Worklist")]
    public class WorklistController : ApiController
    {
        [HttpGet]
        [Route("Get/{userID}")]
        public IHttpActionResult Get(string userID)
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
            parameters.Add("partyID", "992");

            BaseResponse response = adapter.ExecuteCall("Worklist", "Amberpoint", parameters);

            Customer customerResponse = new Customer();

            if (response.ErrCode == "0")
            {
                //Deserialize json
                //customerResponse = JsonConvert.DeserializeObject<Customer>(response.Json);
                return Json(customerResponse);
            }
            else
            {
                throw new HttpRequestException(response.ErrDesc);
            }
        }
    }
}
