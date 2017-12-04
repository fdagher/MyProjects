using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;
using Microsoft.Bot.Builder.FormFlow;
using CarsBot.Bots;

namespace CarsBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            //return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Welcome to Pickles Auctions. Let us help you finding your car.");

            var activity = await result as Activity;

            await context.Forward(Chain.From(() => FormDialog.FromForm(CarBot.BuildForm)), this.ResumeAfterSearchDialog, activity, CancellationToken.None);

            //context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterSearchDialog(IDialogContext context, IAwaitable<object> result)
        {
            var PromptOptions = new string[] { "Yes", "No" };

            PromptDialog.Choice(context, HistoryPrompt, PromptOptions,
                                "Would you like to inquire about the history of any of the cars above?", promptStyle: PromptStyle.Auto);
        }

        private async Task HistoryPrompt(IDialogContext context, IAwaitable<string> result)
        {
            var Prompt = await result;
            string PromptString = Prompt.ToString().ToLower().Trim();

            if (PromptString.Length > 0)
            {

                PromptString = PromptString.Trim();

                if (PromptString == "yes")
                {
                    await context.PostAsync("Please enter the registration number");

                    context.Call(new RegistrationDialog(), this.ResumeAfterRegistrationDialog);
                }
                else
                {
                    await context.PostAsync($"Thanks for contacting our support team.");
                }
            }
        }

        private async Task ResumeAfterRegistrationDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;

                await context.PostAsync("Thanks for contacting our support team.");
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}