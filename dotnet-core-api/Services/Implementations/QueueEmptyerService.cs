using System.Text;
using Azure.Messaging.ServiceBus;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services.Implementations
{

    public class QueueEmptyerService : IQueueEmptyerService
    {
        private const string REMOVED = "Removed the following content from the queue:";
        private readonly ServiceBusClient serviceBusClient;
        private static readonly string queueName = "configurationqueue";

        public QueueEmptyerService(ServiceBusClient serviceBusClient)
        {
            this.serviceBusClient = serviceBusClient;
        }
        public async Task<string> PleaseDoEmptyQueue()
        {

            StringBuilder sb = new StringBuilder(REMOVED);
            ServiceBusReceiver receiver = serviceBusClient.CreateReceiver(queueName);
            while (true)
            {
                ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(1));
                if (message == null) break;
                await receiver.CompleteMessageAsync(message);
                sb.AppendLine();
                sb.Append("<");
                sb.Append("message");
                sb.Append(">");
            }

            if (sb.ToString().Equals(REMOVED))
            {
                sb.Append("nothing");
            }

            // Console.WriteLine("Queue purged successfully.");

            return sb.ToString();
        }
    }
}