using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SASTokenGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the service bus namespace name (ex: picklesdev):");
            var sbnamespace = Console.ReadLine();

            Console.WriteLine("Please enter the service bus primary key:");
            var key = Console.ReadLine();

            Console.WriteLine(CreateToken("https://" + sbnamespace +".servicebus.windows.net/bus-events", "SendListen", key));
            Console.WriteLine(CreateToken("https://" + sbnamespace + ".servicebus.windows.net/exceptions", "SendListen", key));
            Console.WriteLine(CreateToken("https://" + sbnamespace + ".servicebus.windows.net/mail", "SendListen", key));

            Console.ReadLine();
        }

        private static string CreateToken(string resourceUri, string keyName, string key)
        {
            TimeSpan sinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var fiveYears = 60 * 60 * 24 * 7 * 52 * 5;
            var expiry = Convert.ToString((int)sinceEpoch.TotalSeconds + fiveYears);
            string stringToSign = HttpUtility.UrlEncode(resourceUri) + "\n" + expiry;
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            var sasToken = String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}", HttpUtility.UrlEncode(resourceUri), HttpUtility.UrlEncode(signature), expiry, keyName);
            return sasToken;
        }

    }
}
