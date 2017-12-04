using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using Newtonsoft.Json;
using NBK.Web.Api.Adapters;
using NBK.Web.Api.Models.Equation;
using NBK.Web.Api.Helpers;

namespace NBK.Web.Api.Controllers
{
    [RoutePrefix("api/Equation")]
    public class EquationController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CustomerInquiry/{customerNo}/{country}")]
        public IHttpActionResult CustomerInquiry(string customerNo, string country)
        {
            DateTime startTime = DateTime.Now;
            string environment = System.Configuration.ConfigurationManager.AppSettings["Environment." + country];
            CustomerRequest request = new CustomerRequest();
            request.TransactionHeader = new TransactionHeader(country, environment, "1", 1);
            request.CustomerNo = customerNo;

            EquationAdapter adapter = new EquationAdapter();
            Customer response = adapter.CustomerInquiry(request);
            response.ResponseTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;
            if (response.ResponseCode == Constants.Equation_Success)
            {
                return Json(response);
            }
            else
            {
                throw new HttpRequestException(response.ResponseDescription);
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
        [Route("PortfolioInquiry/{customerNo}/{country}/{currency}")]
        public IHttpActionResult PortfolioInquiry(string customerNo, string country, string currency)
        {
            DateTime startTime = DateTime.Now;

            string environment = System.Configuration.ConfigurationManager.AppSettings["Environment." + country];
          
            EquationAdapter adapter = new EquationAdapter();
            Dictionary<string, string> dictionary = this.AccountTypes(country);

            PortfolioRequest request = new PortfolioRequest();
            request.TransactionHeader = new TransactionHeader(country, environment, "1", 1);
            request.CustomerNo = customerNo;
            request.Currency = currency;
            request.MarketSegment = "00";
            request.ServicePackage = "00";
            PortfolioResponse response = adapter.PortfolioInquiry(request);
            response.ResponseTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;

            if (response.ResponseCode == Constants.Equation_Success)
            {
                foreach (Account account in response.Accounts)
                {
                    if (dictionary.ContainsKey(account.Type))
                    {
                        account.Description = dictionary[account.Type].ToString();
                    }
                    else
                    {
                        account.Description = account.Type;
                    }
                }
                return Json(response);
            }
            else
            {
                throw new HttpRequestException(response.ResponseDescription);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="country"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TransactionHistory/{accountNo}/{country}/{currency}")]
        public IHttpActionResult TransactionHistory(string accountNo, string country, string currency)
        {
            DateTime startDate = new DateTime(2014, 01, 01);
            DateTime endDate = DateTime.Now;
            //Temp
            string sdate = String.Format("{0:00}", startDate.Day) +
               String.Format("{0:00}", startDate.Month) + startDate.Year.ToString().Substring(2, 2);

            string edate = String.Format("{0:00}", endDate.Day) +
               String.Format("{0:00}", endDate.Month) + endDate.Year.ToString().Substring(2, 2);


            string environment = System.Configuration.ConfigurationManager.AppSettings["Environment." + country];

            EquationAdapter adapter = new EquationAdapter();
            Dictionary<string, string> dictionary = this.TransactionCodes(country);

            TransactionHistoryRequest request = new TransactionHistoryRequest();
            request.TransactionHeader = new TransactionHeader(country, environment, "1", 1);
            request.Currency = currency;
            request.AccountBranch = accountNo.Substring(0, 4);
            request.AccountBasicNo = accountNo.Substring(4, 6);
            request.AccountSuffix = accountNo.Substring(10, 3);
            request.StartDate = sdate;
            request.EndDate = edate; 
            request.MarketSegment = "00";
            request.ServicePackage = "00";

            TransactionHistoryResponse response = adapter.TransactionHistory(request);
            if (response.ResponseCode == Constants.Equation_Success)
            {
                foreach (Transaction transaction in response.Transactions)
                {

                    if (dictionary.ContainsKey(transaction.Code))
                    {
                        transaction.Description = dictionary[transaction.Code].ToString();
                    }
                    else
                    {
                        transaction.Description = transaction.Code;
                    }
                }
                return Json(response);
            }
            else
            {
                throw new HttpRequestException(response.ResponseDescription);
            }
        }



        #region Equation Helpers 

        private Dictionary<string, string> AccountTypes(string country)
        {
            Dictionary<string, string> dictionary =
               new Dictionary<string, string>();

            var doc = XDocument.Parse(Resource.AccountTypes);

            dictionary = (from item in doc.Descendants("item")
                          where item.Attribute("key") != null && item.Attribute("country").Value == country
                          select new
                          {
                              Key = item.Attribute("key").Value,
                              Data = item.Value,
                          }).ToDictionary(i => i.Key, i => i.Data);

            return dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> DefaultCurrencies()
        {
            Dictionary<string, string> dictionary =
               new Dictionary<string, string>();

            var doc = XDocument.Parse(Resource.DefaultCurrency);

            dictionary = (from item in doc.Descendants("item")
                          where item.Attribute("key") != null
                          select new
                          {
                              Key = item.Attribute("key").Value,
                              Data = item.Value,
                          }).ToDictionary(i => i.Key, i => i.Data);

            return dictionary;
        }


        /// <summary>
        /// Builds dictionary from transactioncodes.xml
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        private Dictionary<string, string> TransactionCodes(string country)
        {
            Dictionary<string, string> dictionary =
               new Dictionary<string, string>();

            var doc = XDocument.Parse(Resource.TransactionCodes);

            dictionary = (from item in doc.Descendants("item")
                          where item.Attribute("code") != null && item.Attribute("country").Value == country
                          select new
                          {
                              Key = item.Attribute("code").Value,
                              Data = item.Value,
                          }).ToDictionary(i => i.Key, i => i.Data);

            return dictionary;
        }
        #endregion
    }
}
