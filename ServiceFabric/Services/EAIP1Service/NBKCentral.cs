using Common;
using NBK.Common.Foundations.Context;
using NBK.Common.Foundations.Exceptions;
using NBK.Common.Foundations.Logging;
using NBK.Common.Foundations.Utilities;
using NBK.EAI.Routing;
using System;
using System.Data;
using WCFExtrasPlus.Soap;

namespace EAIP1Service
{
    public class NBKCentral : INBKCentral
    {
        public WebServiceHeader Header;

        public System.Data.DataSet Create(string targetCategory, System.Data.DataSet inputData)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet Read(string targetCategory, string viewName, NBK.Common.Foundations.Utilities.FilterCriteria[] filterCriteria, NBK.Common.Foundations.Utilities.SortCriteria[] sortCriteria, int startIndex, int numberOfRecords, ref int totalNumberOfRecords)
        {
            try
            {
                Header = SoapHeaderHelper<WebServiceHeader>.GetInputHeader("Header");

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

            return context;

            //Context token
            //context.ContextID = Header.Identifier;

            ////Credentials
            //context.Credentials = new Credential[2];
            //context.Credentials[0] = new Credential(Header.SystemId,
            //    Header.SystemCredentialStore, NBK.Common.Foundations.Context.CredentialType.Channel);
            //context.Credentials[1] = new Credential(Header.UserId,
            //    Header.UserCredentialStore, NBK.Common.Foundations.Context.CredentialType.User);

            ////IT data
            ////context.ITData = new String2String();

            //return context;
        }
    }
}
