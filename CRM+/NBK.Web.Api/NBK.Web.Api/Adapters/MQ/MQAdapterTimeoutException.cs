using System;

namespace NBK.Web.Api.Adapters.MQ
{
    /// <summary>
    /// Summary description for MQAdapterTimeoutException.
    /// </summary>
    public class MQAdapterTimeoutException : ApplicationException
    {
        public MQAdapterTimeoutException(string exception)
            : base(exception)
        {

        }
    }
}
