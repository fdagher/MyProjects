using System;

namespace NBK.Web.Api.Adapters.MQ
{
    /// <summary>
    /// Summary description for MQAdapterException.
    /// </summary>
    public class MQAdapterException : ApplicationException
    {
        public MQAdapterException(string exception)
            : base(exception)
        {

        }
    }
}
