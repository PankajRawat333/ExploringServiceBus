using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;

namespace BasicReceiverWithRetry
{
    public static class Function1
    {
        private static int retry = 0;

        [FunctionName("ReceiverFunction")]
        public static void run([ServiceBusTrigger("TestQueue", AccessRights.Manage, Connection = "servicebusconnection")]string myqueueitem, TraceWriter log)
        {
            retry++;
            System.Console.WriteLine($"Retry attempt {retry}");
            throw new System.Exception("Human error");
            log.Info($"c# servicebus queue trigger function processed message: {myqueueitem}");
        }
    }
}