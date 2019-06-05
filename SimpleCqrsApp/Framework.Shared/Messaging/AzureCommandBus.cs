using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Framework.Shared.Messaging
{
    public class AzureCommandBus : ICommandBus
    {
        public IQueueClient QueueClient { get; }

        public AzureCommandBus(string connectionString, string queueName)
        {
            QueueClient = new QueueClient(connectionString, queueName);
        }

        public async Task SendAsync<T>(T command) where T : ICommand
        {
            var commandMessage = JsonConvert.SerializeObject(command);
            var message = new Message(Encoding.UTF8.GetBytes(commandMessage))
            {
                MessageId = command.CommandId.ToString(),
                ContentType = command.GetType().Name
            };

            await QueueClient.SendAsync(message).ConfigureAwait(false);
        }
    }
}
