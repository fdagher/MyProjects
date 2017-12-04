using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Newtonsoft.Json;
using NBK.Web.Api.Models;

namespace NBK.Web.Api.Adapters
{
    public class XenonAdapter
    {
        public BaseResponse GetCustomerProfile(CustomerPortfolioRequest request)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("token", request.Token);
            parameters.Add("customerNumber", request.CustomerNumber);
            parameters.Add("productListType", request.ProductListType);
            parameters.Add("inquiryMode", request.InquiryMode);
            parameters.Add("custLinkMode", request.CustLinkMode);
            parameters.Add("productStatus", request.ProductStatus);
            BaseResponse response = this.ExecuteCall(request.Transaction, "BFX", parameters);
            return response;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="hosting"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public BaseResponse ExecuteCall(string tranType, string hosting, Hashtable parameters)
        {

            XenonCommunicator xenon = new XenonCommunicator();
            string requestMessage = PrepareSoapRequest(tranType, parameters);
            BaseResponse response = xenon.ExecuteRequest(hosting, requestMessage);

            if (response.ErrCode == "0")
            {
                //TRansform BFX to plain response
                string transformedResponse = this.TransformSoapResponse(tranType, response.SoapMessage);

                if (tranType == "Login")
                {
                    response.Json = transformedResponse;
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(transformedResponse);

                    //REmove xmlns namespace
                    doc = this.RemoveXmlns(doc);

                    //Get error code from message
                    if (doc.SelectSingleNode("//ErrCode") != null)
                    {
                        response.ErrCode = doc.SelectSingleNode("//ErrCode").InnerText;
                        response.ErrDesc = doc.SelectSingleNode("//ErrDesc").InnerText;
                    }
                    //Convert to json
                    string jsonText = JsonConvert.SerializeXmlNode(doc.SelectSingleNode(string.Format("//{0}Response", tranType)),
                        Newtonsoft.Json.Formatting.None, true);

                    //Strip root tag for lists 
                    int startPos = jsonText.IndexOf('[');
                    if (startPos != -1)
                    {
                        jsonText = jsonText.Substring(startPos);
                    }
                    int endPos = jsonText.IndexOf(']');
                    if (endPos != -1)
                    {
                        jsonText = jsonText.Substring(0, endPos + 1);
                    }

                    response.Json = jsonText;
                }
            }

            return response;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string PrepareSoapRequest(String tranType, Hashtable parameters)
        {

            try
            {

                string token = (String)parameters["token"];
                string xslPath = HttpContext.Current.Request.MapPath(string.Format("~/Templates/{0}.Request.xslt", tranType));

                XslCompiledTransform xslTransform = new XslCompiledTransform();
                StringReader reader = new StringReader("<root/>");
                StringWriter writer = new StringWriter();

                xslTransform.Load(xslPath);
                XPathDocument xPathDocument = new XPathDocument(reader);
                XsltArgumentList xsltArgumentList = new XsltArgumentList();
                foreach (string key in parameters.Keys)
                {
                    xsltArgumentList.AddParam(key, "", parameters[key]);
                }

                xslTransform.Transform(xPathDocument, xsltArgumentList, writer);

                return writer.ToString().Replace("utf-16", "utf-8");
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="soapResponse"></param>
        /// <returns></returns>
        public String TransformSoapResponse(String tranType, String soapResponse)
        {
            string xslPath = HttpContext.Current.Request.MapPath(string.Format("~/Templates/{0}.Response.xslt", tranType));

            XslCompiledTransform xslTransform = new XslCompiledTransform();
            StringReader reader = new StringReader(soapResponse);
            StringWriter writer = new StringWriter();

            xslTransform.Load(xslPath);
            XPathDocument xPathDocument = new XPathDocument(reader);

            xslTransform.Transform(xPathDocument, null, writer);

            return writer.ToString().Replace("utf-16", "utf-8");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private XmlDocument RemoveXmlns(XmlDocument doc)
        {
            XDocument d;
            using (var nodeReader = new XmlNodeReader(doc))
                d = XDocument.Load(nodeReader);

            d.Root.Attributes().Where(x => x.IsNamespaceDeclaration).Remove();
            d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            var xmlDocument = new XmlDocument();
            using (var xmlReader = d.CreateReader())
                xmlDocument.Load(xmlReader);

            return xmlDocument;
        }

    }
}