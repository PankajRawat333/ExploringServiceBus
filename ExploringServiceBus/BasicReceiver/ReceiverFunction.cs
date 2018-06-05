using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;

namespace BasicReceiver
{
    public static class ReceiverFunction
    {
        [FunctionName("ReceiverFunction")]
        public static void Run([ServiceBusTrigger("trade-input", AccessRights.Manage, Connection = "ServiceBusConnection")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
