using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HttpRawClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string header = "GET http://picklesloggingapidev.azurewebsites.net/api/logs HTTP/1.1\r\n" +
                            "Host: picklesloggingapidev.azurewebsites.net\r\n" +
                            "Connection: keep-alive\r\n" +
                            "User-Agent: Mozilla/5.0\r\n" +
                            "\r\n";

            //txtResponse.Text = HttpRequest(txtInput.Text);
        }

        /* 
        private string HttpRequest(string request)
        {
            try
            {
                byte[] buf = new byte[65536];

                var tcp = new TcpClient("picklesloggingapidev.azurewebsites.net", 80);

                tcp.SendTimeout = 2000;
                tcp.ReceiveTimeout = 3000;

                // send request
                int res = tcp.Client.Send(Encoding.ASCII.GetBytes(request + "\r\n\r\n"));
                var i = tcp.Client.Receive(buf);
                var response = Encoding.UTF8.GetString(buf, 0, i);
                return response;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        */

        private void btnParseExecute_Click(object sender, EventArgs e)
        {
            string address = "";
            string method = "";
            NameValueCollection headers = null;
            string body = null;

            ParseRawRequest(txtInput.Text, out address, out method, out headers, out body);

            HttpMethod httpmethod = GetHttpMethod(method);
            AuthenticationHeaderValue authHeader = GetAuthorizationHeader(headers["Authorization"]);

            txtResponse.Text = HttpCall(address, httpmethod, headers, body, authHeader);
        }

        private static void ParseRawRequest(string request, out string address, out string method, out NameValueCollection headers, out string body)
        {
            address = null;
            method = null;
            headers = null;
            body = null;

            // get the method and address
            request = ParseMethodAndURL(request, out address, out method);

            // get the body
            body = request.Substring(request.IndexOf("{")).Trim();

            request = request.Substring(0, request.IndexOf("{"));

            // get the headers
            ParseHeaders(request, out headers);
        }

        private static string ParseMethodAndURL(string request, out string address, out string method)
        {
            method = request.Substring(0, request.IndexOf(" "));

            request = request.Substring(request.IndexOf(" ") + 1);

            address = request.Substring(0, request.IndexOf(" "));

            request = request.Substring(request.IndexOf("\n") + 1);

            return request;
        }

        private static void ParseHeaders(string request, out NameValueCollection headers)
        {
            string key, value;
            headers = new NameValueCollection();

            string[] lines = request.Split('\n');

            foreach (string line in lines)
            {
                if (line.Contains(":"))
                {
                    key = line.Substring(0, line.IndexOf(":")).Trim();
                    value = line.Substring(line.IndexOf(":") + 1).Trim();

                    headers.Add(key, value);
                }
            }
        }

        private static HttpMethod GetHttpMethod(string mtd)
        {
            switch (mtd.ToUpper())
            {
                case "POST":
                    return HttpMethod.Post;
                case "GET":
                    return HttpMethod.Get;
                case "PUT":
                    return HttpMethod.Put;
                case "DELETE":
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Get;
            }
        }

        private static AuthenticationHeaderValue GetAuthorizationHeader(string authorization)
        {
            if (!string.IsNullOrEmpty(authorization))
            {
                string scheme = "";
                string token = "";

                ParseAuthorization(authorization, out scheme, out token);

                return new AuthenticationHeaderValue(scheme, token);
            }

            return null;
        }

        private static void ParseAuthorization(string authorizationheader, out string scheme, out string token)
        {
            scheme = authorizationheader.Substring(0, authorizationheader.IndexOf(" "));
            token = authorizationheader.Substring(authorizationheader.IndexOf(" ") + 1);
        }

        private static string HttpCall(string address, HttpMethod method, NameValueCollection headers, string body, AuthenticationHeaderValue authHeader)
        {
            using (HttpClient client = new HttpClient())
            {
                if (authHeader != null)
                {
                    client.DefaultRequestHeaders.Authorization = authHeader;
                }

                foreach (string key in headers.Keys)
                {
                    if (key != "Connection" && key != "Authorization")
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(key, headers[key]);
                    }
                }

                HttpRequestMessage request = new HttpRequestMessage(method, address);

                request.Content = new StringContent(body, Encoding.UTF8, headers["Content-Type"]);

                return client.SendAsync(request).Result.StatusCode.ToString();
            }
        }
    }
}
