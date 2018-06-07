using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeadLetterQueueReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["ServiceBusConnection"];

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connectionString);
            var deadLetterPath = QueueClient.FormatDeadLetterPath("trade-input");
            var dlqReceiver = factory.CreateMessageReceiver(deadLetterPath, ReceiveMode.ReceiveAndDelete);
            BrokeredMessage message;
            while ((message = dlqReceiver.Receive()) != null)
            {
                string body;
                using (var stream = message.GetBody<Stream>())
                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    body = streamReader.ReadToEnd();
                }
                Console.WriteLine($"Message Received {body}");
            }
            Console.Read();
        }


    }
}
