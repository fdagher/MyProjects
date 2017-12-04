using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Configuration;
using NBK.Web.Api.Models;

namespace NBK.Web.Api.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public class XenonCommunicator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hosting"></param>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public BaseResponse ExecuteRequest(string hosting, String requestMessage)
        {
            BaseResponse response = new BaseResponse();
            try
            {

                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "text/xml");
                string uri = string.Empty;
                switch (hosting)
                {
                    case "BFX":
                        uri = System.Configuration.ConfigurationManager.AppSettings["Xenon.BFX"].ToString();
                        break;
                    case "Amberpoint":
                        uri = System.Configuration.ConfigurationManager.AppSettings["Xenon.Amberpoint"].ToString();
                        break;
                    case "EAI":
                        uri = System.Configuration.ConfigurationManager.AppSettings["EAI"].ToString();
                        break;
                }
               



                byte[] postdata = Encoding.UTF8.GetBytes(requestMessage);
                byte[] responsebytes = webClient.UploadData(uri, postdata);

                //Get Response
                response.SoapMessage = Encoding.UTF8.GetString(responsebytes);

                response.ErrCode = "0";
                response.ErrDesc = "OK";

                webClient.Dispose();

                return response;
            }
            catch (WebException webex)
            {
                HttpWebResponse hwr = (HttpWebResponse)webex.Response;
                response.ErrDesc = this.GetErrorDescription(hwr);
                response.ErrCode = "999";
                hwr.Close();
                return response;
            }
           
        }




     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private string GetErrorDescription(HttpWebResponse response)
        {
            string message = string.Empty;
            if (response != null)
            {
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                message = streamReader.ReadToEnd();
                streamReader.Close();
            }
            return message;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public enum ServiceType
    {
        Amberpoint,
        BFX
    }


}