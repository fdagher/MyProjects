using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using CarsBot.Bots;
using Microsoft.Bot.Builder.FormFlow;
using System.Web.Http.Description;
using System;
using System.Threading;
using CarsBot.Dialogs;

namespace CarsBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        //internal static IDialog<CarBot> BuildCarDialog()
        //{
        //    return Chain.From(() => FormDialog.FromForm(CarBot.BuildForm));
        //}

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            try
            {
                //if (activity.Text != null)
                //{
                    //await Conversation.SendAsync(activity, BuildCarDialog);
                //}
                if (activity.Type == ActivityTypes.Message)
                {
                    await Conversation.SendAsync(activity, () => new RootDialog());
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}