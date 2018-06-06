using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BasicReceiver
{
    public static class ReceiverFunction
    {
        //*************Simple string receiver*************
        [FunctionName("ReceiverFunction")]
        //public static void Run([ServiceBusTrigger("trade-input", AccessRights.Manage, Connection = "ServiceBusConnection")]string myQueueItem, TraceWriter log)
        //{
        //    log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        //}

        //*************Brokered message receiver with header values*************
        public static async Task RunAsync([ServiceBusTrigger("trade-input", AccessRights.Manage,
            Connection = "ServiceBusConnection")]BrokeredMessage message, TraceWriter log)
        {
            string body;
            using (var stream = message.GetBody<Stream>())
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                body = await streamReader.ReadToEndAsync();
            }
            string correlationId = message.CorrelationId;
            object tenant;
            message.Properties.TryGetValue("Tenant", out tenant);
            log.Info($"Message CorrelationId :{correlationId}");
            log.Info($"Message Tenant :{tenant}");

            log.Info($"Message body: {body}");
        }
    }
}