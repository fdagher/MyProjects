using System;
using System.Text;
using System.Timers;
using System.Diagnostics;

using IBM.WMQ;
namespace NBK.Web.Api.Adapters.MQ
{
    /// <summary>
    /// Summary description for MQAdapter.
    /// </summary>
    public class MQAdapter
    {
        private MQQueueManager mqQueueManager;
        private MQQueue mqPutQueue;
        private MQQueue mqGetQueue;
        private int pollingTimeout;
        private string mqQueueManagerName;
        private string mqRequestQueueName;
        private string mqResponseQueueName;
        private int characterSet;

        /// <summary>
        /// 
        /// </summary>
        public MQAdapter(HostType host)
        {
            #region Load MQ parameters from configuration  file 
            
            string mqManager = System.Configuration.ConfigurationManager.AppSettings[host.ToString() + ".MQ.Manager"];
            string channel = System.Configuration.ConfigurationManager.AppSettings[host.ToString() + ".MQ.Channel"];
            string ipAddress = System.Configuration.ConfigurationManager.AppSettings[host.ToString() + ".MQ.Server"];
            string putQueue = System.Configuration.ConfigurationManager.AppSettings[host.ToString() + ".MQ.Put.Queue"];
            string getQueue = System.Configuration.ConfigurationManager.AppSettings[host.ToString() + ".MQ.Get.Queue"];
            int timeout = 30000;
            string _timeout = System.Configuration.ConfigurationManager.AppSettings[host.ToString() + ".MQ.Response.Timeout"];
            if (_timeout != null)
            {
                timeout = int.Parse(_timeout);
            }
            int port = 1414;
            string _port = System.Configuration.ConfigurationManager.AppSettings[host.ToString() + ".MQ.Port"];
            if (_port != null)
            {
                port = int.Parse(_port);
            }

            #endregion

            InitializeAdapter(mqManager, channel, ipAddress, putQueue, getQueue, timeout, 37, port);

        }
        /// <summary>
        /// Instantiates the Queue Manager
        /// </summary>
        /// <param name="mqManager">The Queue Manager controlling the Request and Response Queues</param>
        public MQAdapter(string mqManager, string channel, string ipAddress,
            string putQueue, string getQueue, int timeout, int charSet, int port)
        {
            InitializeAdapter(mqManager, channel, ipAddress, putQueue, getQueue, timeout, 37, 1414);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mqManager"></param>
        /// <param name="channel"></param>
        /// <param name="ipAddress"></param>
        /// <param name="putQueue"></param>
        /// <param name="getQueue"></param>
        /// <param name="timeout"></param>
        /// <param name="charSet"></param>
        /// <param name="port"></param>
        private void InitializeAdapter(string mqManager, string channel, string ipAddress,
            string putQueue, string getQueue, int timeout, int charSet, int port)
        {
            try
            {
                MQEnvironment.Hostname = ipAddress;
                MQEnvironment.Channel = channel;
                MQEnvironment.Port = port;
                mqQueueManagerName = mqManager;
                mqRequestQueueName = putQueue;
                mqResponseQueueName = getQueue;
                characterSet = charSet;

                pollingTimeout = timeout;
             
                mqQueueManager = new MQQueueManager(mqManager);//,channel, ipAddress);				

                // Open Queue for Inquiry, Put Message in, and fail if Queue Manager is stopping
                mqPutQueue = mqQueueManager.AccessQueue(putQueue, MQC.MQOO_INQUIRE |
                    MQC.MQOO_OUTPUT | MQC.MQOO_FAIL_IF_QUIESCING | MQC.MQOO_SET_IDENTITY_CONTEXT);

                mqGetQueue = mqQueueManager.AccessQueue(getQueue,
                    MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING);

            }
            catch (MQException mqe)
            {
                NBK.Common.Foundations.Logging.LoggerHelper.Error(mqe);

                throw new MQAdapterException("Error Code: " +
                    MQAdapterErrorReasons.GetMQFailureReasonErrorCode(mqe.Reason));
            }
        }


        /// <summary>
        /// Puts a message in an MQ Queue using the user Id provided
        /// <param name="message">The message to be put in the queue</param>
        /// <returns>Response message</returns>
        /// </summary>
        public string PushMQRequestMessage(string message)
        {
            string location = "NBK.EAI.Adapters.MQAdapter.PushMQRequestMessage";
            try
            {
                MQMessage requestMessage = new MQMessage();

                requestMessage.Persistence = 0;

                requestMessage.ReplyToQueueName = mqResponseQueueName;
                requestMessage.ReplyToQueueManagerName = mqQueueManagerName;

                requestMessage.Format = MQC.MQFMT_STRING;
                requestMessage.CharacterSet = characterSet;
                requestMessage.MessageType = MQC.MQMT_REQUEST;
                requestMessage.MessageId = NBK.Common.Foundations.Utilities.ConversionUtilities
                    .HexToBin(GenerateMQMsgId());
               

                MQPutMessageOptions pmo = new MQPutMessageOptions();
                pmo.Options = MQC.MQPMO_SET_IDENTITY_CONTEXT;

                requestMessage.WriteString(message);

                mqPutQueue.Put(requestMessage, pmo);

                string _msgId = NBK.Common.Foundations.Utilities.
                    ConversionUtilities.BinToHex(requestMessage.MessageId);

                NBK.Common.Foundations.Logging.LoggerHelper.Information
                    (location, "Sent MQ Meesage with MessageId : "
                    + _msgId);

                return _msgId;

            }
            catch (MQException mqe)
            {
                // Close request Queue if still opened
                if (mqPutQueue.OpenStatus)
                    mqPutQueue.Close();
                // Close Queue manager if still opened
                if (mqQueueManager.OpenStatus)
                    mqQueueManager.Close();

                throw new MQAdapterException("Error Code: " +
                    MQAdapterErrorReasons.GetMQFailureReasonErrorCode(mqe.Reason));
            }
        }
        /// <summary>
        /// Get a message from an MQ Queue using a message id
        /// </summary>
        /// <param name="correlationId">correlation id</param>
        /// <returns>Response message</returns>
        public string GetMQResponseMessage(string correlationId)
        {
            string location = "NBK.EAI.Adapters.MQAdapter.GetMQResponseMessage";

            MQMessage rsMsg = new MQMessage();
            rsMsg.MessageId = NBK.Common.Foundations.Utilities.
                    ConversionUtilities.HexToBin(correlationId);

            MQGetMessageOptions gmo = new MQGetMessageOptions();
            gmo.Options = MQC.MQGMO_WAIT;
            gmo.MatchOptions = MQC.MQMO_MATCH_MSG_ID; 
            gmo.WaitInterval = pollingTimeout;

            try
            {
                mqGetQueue.Get(rsMsg, gmo);

                NBK.Common.Foundations.Logging.LoggerHelper.Information
                    (location, "Read MQ Message with CorrelationId: "
                    + NBK.Common.Foundations.Utilities.ConversionUtilities.BinToHex(rsMsg.CorrelationId));

                return rsMsg.ReadString(rsMsg.DataLength);
            }
            catch (MQException mqe)
            {
                // Close Reponse Queue if still opened
                if (mqGetQueue.OpenStatus)
                    mqGetQueue.Close();
                // Close Queue manager if still opened
                if (mqQueueManager.OpenStatus)
                    mqQueueManager.Close();

                NBK.Common.Foundations.Logging.LoggerHelper.Information
                    (location, "Error Reading Message with Correlation ID: "
                    + correlationId);

                // Check if it a timeout exception
                if (MQAdapterErrorReasons.GetMQFailureReasonErrorCode(mqe.Reason) == "MQRC_NO_MSG_AVAILABLE")
                    throw new MQAdapterTimeoutException("Message with correlation Id " + correlationId + " Timed out");

                // MQ Exception
                throw new MQAdapterException("Error Code: " +
                    MQAdapterErrorReasons.GetMQFailureReasonErrorCode(mqe.Reason));
            }
        }
        /// <summary>
        /// Put a message for an MQ Queue and waits for an interval to get the resonse
        /// Throws an exception from type MQAdapterTimeoutException if the intervals expires
        /// </summary>
        /// <param name="message">The message to be put in the queue</param>
        /// <param name="user">The user to be impersonated</param>
        /// <returns>Response message</returns>
        public string SendMQRequestSync(string message)
        {
            string correlationId = PushMQRequestMessage(message);
            return GetMQResponseMessage(correlationId);
        }



        private string GenerateMQMsgId()
        {
            string _mqMsgID = System.Convert.ToString(System.Guid.NewGuid());
            _mqMsgID = _mqMsgID.Replace("-", "");
            _mqMsgID = _mqMsgID.PadRight(48, '0');

            return _mqMsgID;
        }
    }


    public enum HostType
    {
        Equation,
        T24
    }
}