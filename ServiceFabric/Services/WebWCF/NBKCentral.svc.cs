using NBK.Common.Foundations.Context;
using NBK.Common.Foundations.Exceptions;
using NBK.Common.Foundations.Logging;
using NBK.Common.Foundations.Utilities;
using NBK.EAI.Routing;
using NBK.EAI.Services.WebWCF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFExtrasPlus.Soap;

namespace NBK.EAI.Services.WebWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class NBKCentral : INBKCentral
    {
        public WebServiceHeader Header = SoapHeaderHelper<WebServiceHeader>.GetInputHeader("Header");

        public System.Data.DataSet Create(string targetCategory, System.Data.DataSet inputData)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet Read(string targetCategory, string viewName, NBK.Common.Foundations.Utilities.FilterCriteria[] filterCriteria, NBK.Common.Foundations.Utilities.SortCriteria[] sortCriteria, int startIndex, int numberOfRecords, ref int totalNumberOfRecords)
        {
            try
            {
                const string location = "NBK.EAI.Services.Web.NBKCentral.Read";
                using (LogEnterExit lee = new LogEnterExit(location))
                {
                    string _instanceName = "";
                    if (string.IsNullOrEmpty(viewName))
                    {
                        _instanceName = "READ_" + targetCategory + "_VW_" + "Default";
                    }
                    else
                    {
                        _instanceName = "READ_" + targetCategory + "_VW_" + viewName;
                    }

                    //Do data validation before delegating to Request Router
                    CheckArgument(targetCategory != null && targetCategory != "", "Target Category cannot be null or empty");

                    //Delegate the call to the Request Router class
                    DataSet result = RequestRouter.Read(GetCallContext(), targetCategory, viewName,
                        filterCriteria, sortCriteria, startIndex, numberOfRecords,
                        ref totalNumberOfRecords);

                    return result;
                }
            }
            catch (Exception ex)
            {
                BaseException.Publish(ex);

                //LogCallContext();

                throw ExceptionUtils.GetSOAPException(ex);
            }
        }

        public System.Data.DataSet Update(string targetCategory, System.Data.DataSet inputData)
        {
            throw new NotImplementedException();
        }

        public int Delete(string targetCategory, NBK.Common.Foundations.Utilities.FilterCriteria[] filterCriteria)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet InvokeMethod(string targetCategory, string actionType, System.Data.DataSet inputData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Utility function to check arguments through the passed expression and 
        /// throws an ArgumentException in case the expression evaluated to false/>
        /// </summary>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="error">Error message to throw</param>
        protected void CheckArgument(bool expression, string error)
        {
            if (!expression)
            {
                throw new ArgumentException(error);
            }
        }

        /// <summary>
        /// Prepare the call context utilizing the SOAP header information
        /// </summary>
        /// <returns>Filled <see cref="CallContext"/> object</returns>
        protected void LogCallContext()
        {
            //Log System ID
            LoggerHelper.Error("Request Originated by: " + Header.SystemCredentialStore + "\\" + Header.SystemId);

            //Log IT data
            //foreach (String2String.Entry _itDatum in Header.ITData.Entries)
            //{
            //    LoggerHelper.Error(_itDatum.ToString());
            //}
        }

        /// <summary>
        /// Prepare the call context utilizing the SOAP header information
        /// </summary>
        /// <returns>Filled <see cref="CallContext"/> object</returns>
        protected CallContext GetCallContext()
        {
            CallContext context = new CallContext();

            //Context token
            context.ContextID = Header.Identifier;

            //Credentials
            context.Credentials = new Credential[2];
            context.Credentials[0] = new Credential(Header.SystemId,
                Header.SystemCredentialStore, CredentialType.Channel);
            context.Credentials[1] = new Credential(Header.UserId,
                Header.UserCredentialStore, CredentialType.User);

            //IT data
            //context.ITData = new String2String();

            return context;
        }
    }
}
