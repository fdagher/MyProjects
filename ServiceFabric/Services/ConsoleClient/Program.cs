using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting the access token using the Client Credentials Flow (Machine2Machine Use Case)...");
            TokenResponse response = GetClientToken();
            Console.WriteLine();

            Console.WriteLine("Access token is:");
            Console.WriteLine(response.AccessToken);
            Console.WriteLine();

            Console.WriteLine("Calling the service and sending the access token...");
            Console.WriteLine(CallApi(response));
            Console.WriteLine();

            Console.WriteLine("Getting the access token using the Resource Owner Password Credentials Flow (User/Pass Entered in Trusted App Use Case)...");
            response = GetUserToken();
            Console.WriteLine();

            Console.WriteLine("Access token is:");
            Console.WriteLine(response.AccessToken);
            Console.WriteLine();

            Console.WriteLine("Calling the service and sending the access token...");
            Console.WriteLine(CallApi(response));

            Console.ReadLine();
        }

        static TokenResponse GetClientToken()
        {
            var client = new TokenClient("http://localhost:5000/connect/token",
                                         "crm_service", 
                                         "B443D9C2-D068-4542-B148-D58003022CEA");

            return client.RequestClientCredentialsAsync("customersapi").Result;
        }

        static string CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            //return client.GetStringAsync("http://localhost:8323/api/test").Result;
            return client.GetStringAsync("http://localhost:8380/services/customer/get/000000094").Result;
        }

        static TokenResponse GetUserToken()
        {
            var client = new TokenClient("http://localhost:5000/connect/token",
                                         "carbon",
                                         "E855AB91-0ACE-44AD-BBE6-7D9C0F34513B");

            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "customersapi").Result;
        }
    }
}
