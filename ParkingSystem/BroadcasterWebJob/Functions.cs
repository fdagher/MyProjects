using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Net.Http;
using System;
using System.Net.Http.Headers;

namespace BroadcasterWebJob
{
    public class Functions
    {
        static HttpClient client = new HttpClient();

        static Functions()
        {
            client.BaseAddress = new Uri("http://localhost:60261/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // This function will be triggered based on the schedule you have set for this WebJob
        // This function will call the broadcast resource in the parking api to kickoff 
        // the daily booking process
        [NoAutomaticTrigger]
        public async static void BroadcastParkingNotifications(TextWriter log)
        {
            try
            {
                log.WriteLine("Parking notification process started for " + DateTime.Today);

                await SendNotificationsAsync();
            }
            catch(Exception ex)
            {
                log.WriteLine("Error: " + ex.ToString());
            }
        }

        static async Task<Uri> SendNotificationsAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/users/broadcast");

            response.EnsureSuccessStatusCode();

            // Return the URI of the created resource.
            return response.Headers.Location;
        }
    }
}
