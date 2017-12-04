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
    public enum HistoryViewResponse
    {
        Yes = 1,
        No = 2
    }

    [Serializable]
    public class CarBot
    {
        [Prompt("What car are you interested in? {||}")]
        public CarType Make;

        [Prompt("Do you have a body preference? {||}")]
        public BodyType Body;

        [Prompt("What is your budget?")]
        public int? Budget;

        [Prompt("What is maximum kilometers driven?")]
        public int? Kilometers;

        [Prompt("Do you want to view history logs for any specific car? {||}")]
        public HistoryViewResponse GetHistory;

        [Prompt("What is the registration number of the car you want to get its history?")]
        public string RegistrationNumber;

        public bool AskToGetHistory;

        public static IForm<CarBot> BuildForm()
        {
            OnCompletionAsyncDelegate<CarBot> wrapUpRequest = async (context, state) =>
            {
                string wrapUpMessage = @"Please hold on for a sec until we get you the available cars...";

                await context.PostAsync(wrapUpMessage);

                IList<Car> results = CarRepository.Search(state.Make, state.Body, state.Budget, state.Kilometers);

                if (results.Count > 0)
                {
                    StringBuilder builder = BuildResult(results);

                    await context.PostAsync("Here you go...");
                    await context.PostAsync(builder.ToString());
                    //await context.PostAsync("Thanks for chating with us. Chat again to restart your car search.");
                }
                else
                {
                    await context.PostAsync("We are sorry, we couldn't find any cars matching your criteria in our stores.");
                }
            };
            
            return new FormBuilder<CarBot>()
                .Field(nameof(Make))
                .Field(nameof(Body))
                .Field(nameof(Budget))
                .Field(nameof(Kilometers))
                //.Field(nameof(GetHistory))
                //.Field(new FieldReflector<CarBot>(nameof(GetHistory))
                //    .SetActive(state => state.AskToGetHistory)

                //    .SetNext((value, state) =>
                //    {
                //        var selection = (HistoryViewResponse)value;

                //        if (selection == HistoryViewResponse.Yes)
                //        {
                //            return new NextStep(new[] { nameof(RegistrationNumber) });
                //        }
                //        else
                //        {
                //            return new NextStep();
                //        }
                //    }))              
                .OnCompletion(wrapUpRequest)
                .Build();
        }

        private static StringBuilder BuildResult(IList<Car> results)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<table style='width: 800px'>");
            builder.Append("<tr><td><b>Make</b></td><td><b>Model</b></td><td><b>Transmission</b></td><td><b>Engine</b></td><td><b>Body</b></td><td><b>Price</b></td><td><b>Kilometers</b></td><td><b>Rego#</b></td><td><b>Details</b></td></tr>");

            foreach (Car c in results)
            {
                builder.Append(c.ToString());
            }

            builder.Append("</table>");
            return builder;
        }

        //public static async Task DoesUserWantHistory(IDialogContext context, IAwaitable<IMessageActivity> argument)
        //{
        //    IMessageActivity message = await argument;

        //    switch (message.Text.ToLower())
        //    {
        //        case "yes":
        //            await context.PostAsync($"Great. Go ahead and take a picture of the first couple of pages and attach them to this conversation.\n\n\nWhen you have finished, please send the message 'finished'.");
        //            context.Wait(CarHistoryAsync);
        //            await context.PostAsync("Thanks for chating with us. Chat again to restart your car search.");
        //            break;
        //        case "no":
        //            await context.PostAsync($"That's OK. Have a nice day.");
        //            break;
        //        default:
        //            await context.PostAsync($"Sorry, I didn't undestand. Please reply with 'yes' or 'no'.");
        //            context.Wait(DoesUserWantHistory);
        //            break;
        //    }
        //}

        //private static async Task CarHistoryAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        //{
        //    await context.PostAsync("Please provide the registration numberRegistration info...");
        //    IMessageActivity message = await argument;
        //    var rn = message.Text;
        //    await context.PostAsync($"Fetching the history of car with registration number {message.Text}.");
        //    await context.PostAsync(CarRepository.CarInfo(rn));
        //}
    }
}