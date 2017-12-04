using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types

namespace ImageReconciliation
{
    class Program
    {
        static string dbConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        static void Main(string[] args)
        {
            int allrecords;
            int numOfBatches = 1;
            int batchSize = int.Parse(ConfigurationManager.AppSettings["BatchSize"]);

            if (args != null && args.Length > 0)
            {
                allrecords = int.Parse(args[0]);
            }
            else
            {
                allrecords = GetTotalRecords();
            }

            if (allrecords > batchSize)
            {
                numOfBatches = allrecords / batchSize;

                if (allrecords % batchSize > 0)
                {
                    numOfBatches += 1;
                }

                allrecords = batchSize;
            }

            DoWork(allrecords, numOfBatches);
        }

        private static void DoWork(int count, int numOfBatches)
        {
            var tasks = new List<Task>();

            for (int i = 0; i < numOfBatches; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => ProcessImages(count)));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine();

            WriteToConsole($"Finished processing a total of {count * numOfBatches} records...", ConsoleColor.Green);
        }

        private static int ProcessImages(int batchSize)
        {
            int reconcilationId;
            string container_id, cloudinaryURL, file_name, blobUrl;
            string cloudinaryExists, blobExists;
            bool result;

            int uniqueId = GenerateId();

            try
            {
                WriteToConsole($"Started processing {batchSize} records in thread {System.Threading.Thread.CurrentThread.ManagedThreadId}...", ConsoleColor.Green);

                WriteToConsole($"Unique id in thread {System.Threading.Thread.CurrentThread.ManagedThreadId} is {uniqueId} ...", ConsoleColor.Yellow);

                int countResults = MarkBatchDirty(uniqueId, batchSize);

                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();

                myConnection.ConnectionString = dbConnectionString;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;


                sqlCmd.CommandText = $@"SELECT [ReconcilationId]
                                        ,ISNULL([pas_item_id],[uService_item_id]) as container_id
                                        ,[cloudinaryExists]
                                        ,[blobExists]
                                        ,[cloudinaryURL]
                                        ,[file_name]
                            FROM [reconcilation]
                            WHERE [cloudinaryExists] = {uniqueId} OR [blobExists] = {uniqueId}";


                sqlCmd.Connection = myConnection;
                myConnection.Open();
                reader = sqlCmd.ExecuteReader();



                while (reader.Read())
                {
                    reconcilationId = int.Parse(reader.GetValue(0).ToString());
                    container_id = reader.GetValue(1).ToString();
                    cloudinaryExists = reader.GetValue(2).ToString();
                    blobExists = reader.GetValue(3).ToString();
                    cloudinaryURL = reader.GetValue(4).ToString();
                    file_name = reader.GetValue(5).ToString();

                    if (!string.IsNullOrEmpty(cloudinaryExists) && int.Parse(cloudinaryExists) == uniqueId)
                    {
                        result = HttpGet(cloudinaryURL);

                        WriteToConsole($"Thread {Thread.CurrentThread.ManagedThreadId}: {cloudinaryURL} status result: {result}", ConsoleColor.White);
                        UpdateStatus(reconcilationId, "cloudinaryExists", result);
                    }

                    if (!string.IsNullOrEmpty(blobExists) && int.Parse(blobExists) == uniqueId)
                    {
                        blobUrl = $"https://aaeimgmgntdaasstorstdprd.blob.core.windows.net/{container_id}/{file_name}";

                        result = StorageGet(blobUrl);

                        WriteToConsole($"Thread {Thread.CurrentThread.ManagedThreadId}: {blobUrl} status result: {result}", ConsoleColor.White);
                        UpdateStatus(reconcilationId, "blobExists", result);
                    }
                }

                myConnection.Close();

                WriteToConsole($"Finished processing {batchSize} records in thread {Thread.CurrentThread.ManagedThreadId}...", ConsoleColor.Green);

                return countResults;
            }
            catch
            {
                RollbackStatus(uniqueId);
                return 0;
            }
        }

        private static int GenerateId()
        {
            return int.Parse(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
        }

        private static int GetTotalRecords()
        {
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = dbConnectionString;

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = $@"SELECT COUNT(1) FROM [reconcilation]
                                   WHERE ([cloudinaryExists] IS NULL and [file_name] NOT LIKE '%load_%')";

            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int res = int.Parse(sqlCmd.ExecuteScalar().ToString());
            myConnection.Close();

            return res;
        }

        private static int MarkBatchDirty(int uniqueId, int batchSize)
        {
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = dbConnectionString;

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = $@"UPDATE [reconcilation]
                                    SET [cloudinaryExists] = @id, [blobExists] = @id
                                    WHERE [ReconcilationId] IN (SELECT TOP {batchSize} [ReconcilationId]
                                                                FROM [reconcilation]
                                                                WHERE (([cloudinaryExists] IS NULL OR [blobExists] IS NULL) and [file_name] NOT LIKE '%load_%')
                                                                ORDER BY [ReconcilationId] DESC)";

            sqlCmd.Parameters.Add(new SqlParameter("id", uniqueId));

            sqlCmd.Connection = myConnection;

            myConnection.Open();
            int res = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return res;
        }

        private static bool HttpGet(string url)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "94bcb8e6907c484d9c7ab8b657d1e2f2");

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6InJFMzg3Y0o2dUhEbUVNS1lscmxHSkEzcGQxZEwtY0FHQV9uY1NCdkNiODAifQ.eyJ2ZXIiOjEsImp0aSI6IkFULlUyOGRaaFM3T0dFUEN3VVlRRnRwSXJBZDJRMDZNS01VVEJURlVtQjVRR3MiLCJpc3MiOiJodHRwczovL3BpY2tsZXMub2t0YXByZXZpZXcuY29tL29hdXRoMi9hdXM5bWJnbTUyTTk2c3d2dTBoNyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3QiLCJpYXQiOjE0OTQ5ODI5NTYsImV4cCI6MTQ5NTA2OTM1NiwiY2lkIjoiQnZGRVZ2VHgzRTY3R2paNXdqTkwiLCJzY3AiOlsiY3VzdG9tX3Njb3BlIl0sInN1YiI6IkJ2RkVWdlR4M0U2N0dqWjV3ak5MIn0.D45BicNq6QMl7Wrhz5kLtwyK-KrKAMhAsgmrCFbTpDR2e97EIShW_hYI1Qo7T0bzEXPZSZz06E4ZU4Ihds5NnIh06jsuVpCKsC3klM7nZM8q_Ty-qRR9uiVmYzAWISMsuXF4LohwbSMeinjf3rpmKStvb7q0-9pEolu06BBaOv2pqbmtlXBlg1MLs0AQYoYB9ei59lQ7NRyDqWhiY7XlHgu1DkRQtlFeDv6GEUfOnSvcNUSlm6OWyxoAdclGF6q738D_5cg6nycNTPfskq34RVfDqfDJ4F1_ACPB5F3zvWV7yRlWIppI18_FpejVfo7YvLWRuDbUk7EbDVElNAQp1Q");

            HttpResponseMessage response = client.GetAsync(url).Result;

            return response.IsSuccessStatusCode;
        }

        private static bool StorageGet(string url)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            string containerName = url.Split('/').Reverse().Skip(1).FirstOrDefault();
            string blobName = url.Split('/').Reverse().FirstOrDefault();

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            
            if (container.ListBlobs(blobName).ToList().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void UpdateStatus(int id, string columnName, bool value)
        {
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = dbConnectionString;

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandType = CommandType.Text;
            
            sqlCmd.CommandText = $@"UPDATE [reconcilation]
                                   SET [{columnName}] = @result
                                  WHERE [ReconcilationId]=@id";

            sqlCmd.Parameters.Add(new SqlParameter("result", value));
            sqlCmd.Parameters.Add(new SqlParameter("id", id));

            sqlCmd.Connection = myConnection;
            myConnection.Open();
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        private static void RollbackStatus(int id)
        {
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = dbConnectionString;

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = $@"UPDATE [reconcilation]
                                   SET [cloudinaryExists] = NULL, [blobExists] = NULL
                                  WHERE [ReconcilationId]=@id";

            sqlCmd.Parameters.Add(new SqlParameter("id", id));

            sqlCmd.Connection = myConnection;
            myConnection.Open();
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        private static void WriteToConsole(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
