using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BatchReceiver
{
    public static class BatchReceiver
    {
        private static int i = 0;

        [FunctionName("ServiceBusConnection")]
        public static async Task RunAsync([ServiceBusTrigger("batch-input", AccessRights.Manage,
            Connection = "ServiceBusConnection")]BrokeredMessage message, TraceWriter log)
        {
            string body;
            using (var stream = message.GetBody<Stream>())
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                body = await streamReader.ReadToEndAsync();
            }
            SampleMessage sampleMessage = JsonConvert.DeserializeObject<SampleMessage>(body);

            //if (i == 99)
            //{
            //    throw new Exception("Human error");
            //}
            await SaveMessageIntoAzureTableAsync(sampleMessage);
            //await message.CompleteAsync();
            log.Info($"Message body: {sampleMessage}");
        }

        private static async Task SaveMessageIntoAzureTableAsync(SampleMessage sampleMessage)
        {
            await Task.Delay(1000);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnection"]);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("people");
            await table.CreateIfNotExistsAsync();

            //Batch insert more efficient and cheapest, but here we are testing queue batch receive.
            CustomerEntity customer = new CustomerEntity(sampleMessage.Id, "IN");
            customer.Name = sampleMessage.Name;
            var insertOperation = TableOperation.Insert(customer);
            table.Execute(insertOperation);
        }

        ///https://stackoverflow.com/a/10941311/4140278
    }
}