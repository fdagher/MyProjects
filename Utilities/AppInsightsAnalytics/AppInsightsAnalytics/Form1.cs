using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppInsightsAnalytics
{
    public partial class Form1 : Form
    {
        private const string URL = "https://api.applicationinsights.io/beta/apps/{0}/{1}?{2}";

        private string appId = System.Configuration.ConfigurationManager.AppSettings["ApplicationId"];
        private string key = System.Configuration.ConfigurationManager.AppSettings["APIKey"];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static string GetTelemetry(string appid, string apikey,
                string queryType, string parameterString)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", apikey);

            var req = string.Format(URL, appid, queryType, parameterString);
            
            HttpResponseMessage response = client.GetAsync(req).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return response.ReasonPhrase;
            }
        }

        private void btnCallAPI_Click(object sender, EventArgs e)
        {
            var json = GetTelemetry(appId, key, "query", "query=" + txtQuery.Text);

            json = Beautify(json);

            txtResponse.Text = json;
        }

        private string Beautify(string json)
        {
            object obj = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                string finalObject = TransformJson(txtResponse.Text);
                watch.Stop();

                MessageBox.Show("Transformation took: " + watch.Elapsed.Milliseconds);

                LogResult[] results = JsonConvert.DeserializeObject<LogResult[]>(finalObject);

                string revertBack = JsonConvert.SerializeObject(results);

                revertBack = Beautify(revertBack);

                txtTransformed.Text = revertBack;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private string TransformJson(string input)
        {
            string finalObject = "";
            StringBuilder body = new StringBuilder();
            JObject parent = JObject.Parse(input);

            JToken table = parent.First.First.First;

            JToken columns = table["Columns"];
            JToken rows = table["Rows"];

            int i = 0;

            foreach (JObject column in columns)
            {
                body.Append("'" + column.First.First + "' : '{" + i + "}', ");

                i += 1;
            }

            string strBody = body.ToString().Substring(0, body.ToString().Length - 2);

            var jsonObjectTemplate = "{" + strBody + "}";

            for (i = 0; i < rows.Count(); i++)
            {
                var jsonObject = jsonObjectTemplate;

                var row = rows[i];

                int j = 0;

                foreach (JValue value in row)
                {
                    jsonObject = jsonObject.Replace("{" + j + "}", (value.Value == null) ? "" : value.Value.ToString());

                    j += 1;
                }

                finalObject += jsonObject + ",";
            }

            finalObject = finalObject.Substring(0, finalObject.Length - 1);

            finalObject = "[" + finalObject + "]";
            return finalObject;
        }
    }
}
