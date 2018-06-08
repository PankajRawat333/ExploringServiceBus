using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace BasicSenderWithRetryPolicy
{
    internal class Program
    {
        private const string QueueName = "TestQueue";

        private static void Main(string[] args)
        {
            Console.WriteLine("Start sending messages");
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            string connectionString = ConfigurationManager.AppSettings["ServiceBusConnection"];
            var nm = NamespaceManager.CreateFromConnectionString(connectionString);
            var queue = new QueueDescription(QueueName);
            queue.MaxDeliveryCount = 3;
            if (!nm.QueueExists(QueueName))
                await nm.CreateQueueAsync(queue);

            QueueClient queueClient = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            string tradeData = File.ReadAllText("TradeSchemaDemo.json");
            var message = new BrokeredMessage(tradeData);
            await queueClient.SendAsync(message);
            await queueClient.CloseAsync();
        }
    }
}