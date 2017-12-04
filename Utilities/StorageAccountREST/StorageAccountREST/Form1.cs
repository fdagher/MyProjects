using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageAccountREST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnPut_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var RequestDateString = DateTime.UtcNow.ToString("R", CultureInfo.InvariantCulture);

            client.DefaultRequestHeaders.Add("x-ms-date", RequestDateString);
            //client.DefaultRequestHeaders.Add("x-ms-version", "2015-02-21");

            var StorageAccountName = "devpicklesintstacct";
            var StorageKey = "aWvAHX7CkucY/8k4COoWOy0HYi+JQGxCLIqivYJZhdi14FZ4a96TnmWEOgl0ZJeYqtEA2kWwhsEbXFsg5I73Wg==";
            var requestUri = new Uri("https://devpicklesintstacct.blob.core.windows.net/axlargemessages/helloblob");

            if (client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Remove("Authorization");

            //var canonicalizedStringToBuild = string.Format("PUT\n\n\n\n\n\n\n\n\n\n\n\nx-ms-date:{0}\nx-ms-version:2015-02-21\n/{1}/{2}\nrestype:container", RequestDateString, StorageAccountName, "mycontainer");
            var canonicalizedStringToBuild = string.Format("PUT\n\ntext/plain; charset=UTF-8\n\nx-ms-date:{0}\n/{1}/{2}/{3}", RequestDateString, StorageAccountName, "axlargemessages", "helloblob");
            string signature;

            using (var hmac = new HMACSHA256(Convert.FromBase64String(StorageKey)))
            {
                byte[] dataToHmac = Encoding.UTF8.GetBytes(canonicalizedStringToBuild);
                signature = Convert.ToBase64String(hmac.ComputeHash(dataToHmac));
            }

            string authorizationHeader = string.Format($"{StorageAccountName}:" + signature);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SharedKeyLite", authorizationHeader);

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PutAsync(requestUri, new StringContent("Hello world", Encoding.UTF8, "text/plain"));

            MessageBox.Show(response.StatusCode.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            GetBlob("devpicklesintstacct", "axlargemessages", "aWvAHX7CkucY/8k4COoWOy0HYi+JQGxCLIqivYJZhdi14FZ4a96TnmWEOgl0ZJeYqtEA2kWwhsEbXFsg5I73Wg==");
        }

        void GetBlob(string accountName, string container, string accessKey)
        {
            Debug.WriteLine("Attempting to GET from server");
            DateTime dt = DateTime.UtcNow;
            string stringToSign = String.Format("GET\n"
                                                + "\n" // content md5
                                                + "\n" // content type
                                                + "x-ms-date:" + dt.ToString("R") + "\nx-ms-version:2016-05-31\n" // headers
                                                + "/{0}/{1}\ncomp:list\nrestype:container", accountName, container);
            string authorizationKey = SignThis(stringToSign, accessKey, accountName);
            string method = "GET";
            string urlPath = string.Format("https://{0}.blob.core.windows.net/{1}?restype=container&comp=list", accountName, container);
            Uri uriTest = new Uri(urlPath);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriTest);
            request.Method = method;
            request.Headers.Add("x-ms-date", dt.ToString("R"));
            request.Headers.Add("x-ms-version", "2016-05-31");
            //request.Headers.Add("Authorization", authorizationKey);
            request.Headers.Add("Authorization", GetHeader());
            Debug.WriteLine("Authorization: " + authorizationKey);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Debug.WriteLine("Response = " + response);
            }
        }
        private static String SignThis(String StringToSign, string Key, string Account)
        {
            String signature = string.Empty;
            byte[] unicodeKey = Convert.FromBase64String(Key);
            using (HMACSHA256 hmacSha256 = new HMACSHA256(unicodeKey))
            {
                Byte[] dataToHmac = System.Text.Encoding.UTF8.GetBytes(StringToSign);
                signature = Convert.ToBase64String(hmacSha256.ComputeHash(dataToHmac));
            }

            String authorizationHeader = String.Format(
                CultureInfo.InvariantCulture,
                "{0} {1}:{2}",
                "SharedKeyLite",
                Account,
                signature);

            return authorizationHeader;
        }

        private string GetHeader()
        {
            var account = "devpicklesintstacct";
            var key = "aWvAHX7CkucY/8k4COoWOy0HYi+JQGxCLIqivYJZhdi14FZ4a96TnmWEOgl0ZJeYqtEA2kWwhsEbXFsg5I73Wg==";
            var container = "axlargemessages";
            var file = "testblob";
            var dateToSign = DateTime.UtcNow.ToString("R");
            var stringToSign = string.Format("GET\n\n\n{0}\n/{1}/{2}/{3}", dateToSign, account, container, file);
            string signature;
            var unicodeKey = Convert.FromBase64String(key);
            using (var hmacSha256 = new HMACSHA256(unicodeKey))
            {
                var dataToHmac = Encoding.UTF8.GetBytes(stringToSign);
                signature = Convert.ToBase64String(hmacSha256.ComputeHash(dataToHmac));
            }
            var authorizationHeader = string.Format(
                "{0} {1}:{2}",
                "SharedKey",
                account,
                signature);
            return authorizationHeader;
        }
    }
}
