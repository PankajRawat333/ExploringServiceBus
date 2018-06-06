using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace BasicSender
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start sending messages");
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            string connectionString = ConfigurationManager.AppSettings["ServiceBusConnection"];
            string inputQueue = "trade-input";//make sure queue already created
            const int numberOfMessages = 2;

            QueueClient queueClient = QueueClient.CreateFromConnectionString(connectionString, inputQueue);
            //await SendMessagesAsync(queueClient, numberOfMessages);
            await SendMessagesWithHeaderAsync(queueClient, numberOfMessages);
            await queueClient.CloseAsync();
        }

        private static async Task SendMessagesAsync(QueueClient queueClient, int numberOfMessagesToSend)
        {
            try
            {
                string tradeData = File.ReadAllText("TradeSchemaDemo.json");
                for (int i = 0; i < numberOfMessagesToSend; i++)
                {
                    var message = new BrokeredMessage(tradeData);
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

        private static async Task SendMessagesWithHeaderAsync(QueueClient queueClient, int numberOfMessagesToSend)
        {
            try
            {
                string tradeData = File.ReadAllText("TradeSchemaDemo.json");
                for (int i = 0; i < numberOfMessagesToSend; i++)
                {
                    var message = new BrokeredMessage(tradeData);
                    message.CorrelationId = Guid.NewGuid().ToString();
                    message.Properties.Add("Tenant", "OpenBank");
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}