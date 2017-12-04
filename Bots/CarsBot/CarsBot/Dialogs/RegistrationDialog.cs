using CarsBot.Repository;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarsBot.Bots
{
    [Serializable]
    public class RegistrationDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            IMessageActivity message = await argument;

            var rn = message.Text;

            var res = CarRepository.CarInfo(rn);

            await context.PostAsync($"Thanks. Here is your car info");

            await context.PostAsync(res);

            context.Done(rn);
            //context.Wait(CaptureRegistrationNumberAsync);
        }
        public async Task CaptureRegistrationNumberAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            IMessageActivity message = await argument;

            var rn = message.Text;

            var res = CarRepository.CarInfo(rn);

            await context.PostAsync($"Thanks. Here is your car info");

            await context.PostAsync(res);
        }
    }
}