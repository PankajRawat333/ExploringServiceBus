using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BatchSender
{
    internal class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            ////****************************************
            ///The maximum size of the batch is the same as the maximum size of a single message(currently 256 Kb).
            ////****************************************
            System.Console.WriteLine("Start Sending messages");
            MainAsync().GetAwaiter().GetResult();
            Console.WriteLine("Done! Press enter to exit.");
            Console.Read();
        }

        private static async Task MainAsync()
        {
            string connectionString = ConfigurationManager.AppSettings["ServiceBusConnection"];
            string inputQueue = "batch-input";//make sure queue already created

            QueueClient queueClient = QueueClient.CreateFromConnectionString(connectionString, inputQueue);
            List<BrokeredMessage> brokeredMessagesList = new List<BrokeredMessage>();
            for (int i = 1; i <= 1000; i++)
            {
                SampleMessage sampleMessage = new SampleMessage
                {
                    Id = i,
                    Name = $"PankajRawat{i}"
                };
                var payload = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sampleMessage)));
                brokeredMessagesList.Add(new BrokeredMessage(payload, true));
            }
            
            await queueClient.SendBatchAsync(brokeredMessagesList);
            await queueClient.CloseAsync();
        }
    }
}