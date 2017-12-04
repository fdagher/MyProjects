using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CCROPFlowClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Client Credentials Flow
            CallResourceAPIUsingClientCredentialsFlow();

            //Resource Owner Pass
            CallResourceAPIUsingResourceOwnerPasswordFlow();

            Console.ReadLine();
        }

        private static void CallResourceAPIUsingClientCredentialsFlow()
        {
            string _accessToken = GetAccessTokenUsingClientCredentials();

            CallAPI(_accessToken);
        }

        private static void CallResourceAPIUsingResourceOwnerPasswordFlow()
        {
            string _accessToken = GetAccessTokenUsingROPassword();

            CallAPI(_accessToken);
        }

        private static string GetAccessTokenUsingClientCredentials()
        {
            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:5001").Result;

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "cc.client", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            Console.WriteLine(tokenResponse.Json);

            return tokenResponse.AccessToken;
        }

        private static string GetAccessTokenUsingROPassword()
        {
            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:5001").Result;

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            Console.WriteLine(tokenResponse.Json);

            return tokenResponse.AccessToken;
        }

        private static void CallAPI(string accessToken)
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = client.GetAsync("http://localhost:5500/api/identity").Result;

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

            var content = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(JArray.Parse(content));
        }
    }
}
