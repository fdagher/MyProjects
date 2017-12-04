using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EH = Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Configuration;

namespace Pickles.EventHub.Client
{
    public partial class Form1 : Form
    {
        private static EH.EventHubClient eventHubClient;
        private string EhConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private string EhEntityPath = ConfigurationManager.AppSettings["EntityPath"];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var connectionStringBuilder = new EH.EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath
            };

            eventHubClient = EH.EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            string x = eventHubClient.EventHubName;

            var ri = eventHubClient.GetRuntimeInformationAsync().Result;

            //await SendMessagesToEventHub(100);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            eventHubClient.CloseAsync();
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            var ri = eventHubClient.GetRuntimeInformationAsync().Result;
            long lastseq = eventHubClient.GetPartitionRuntimeInformationAsync("0").Result.LastEnqueuedSequenceNumber;
        }
    }
}
