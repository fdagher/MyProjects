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
    [RoutePrefix("api/ProductList")]
    public class ProductListController : ApiController
    {

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

            List<Product> productList = new List<Product>();
            CustomerPortfolioRequest request = new CustomerPortfolioRequest();
            request.Token = token;
            request.CustomerNumber = customerNo;
            request.ProductListType = "All";
            request.InquiryMode = "Simple";
            request.CustLinkMode = "Direct";
            request.ProductStatus = "Active";
            XenonAdapter adapter = new XenonAdapter();
            BaseResponse response = adapter.GetCustomerProfile(request);
            if (response.ErrCode == "0")
            {
                //Deserialize json
                productList = JsonConvert.DeserializeObject<List<Product>>(response.Json);
            }

            return Json(productList);
        }

        [HttpGet]
        [Route("AccountDetails/{accountNo}")]
        public IHttpActionResult AccountDetails(string accountNo)
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
            parameters.Add("accountNumber", accountNo);

            BaseResponse response = adapter.ExecuteCall("AccountInquiry", "BFX", parameters);

            Account account = new Account();

            if (response.ErrCode == "0")
            {
                //Deserialize json
                account = JsonConvert.DeserializeObject<Account>(response.Json);
                account.AccountNo = accountNo;
                return Json(account);
            }
            else
            {
                throw new HttpRequestException(response.ErrDesc);
            }
        }

    }



}
