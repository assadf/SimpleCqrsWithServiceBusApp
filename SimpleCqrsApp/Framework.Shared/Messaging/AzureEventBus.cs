using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Framework.Shared.Messaging
{
    public class AzureEventBus : IEventBus
    {
        private readonly string _connectionString;

        public AzureEventBus(string connectionString)
        {
            _connectionString = connectionString;
        }


        public Task SendAsync<T>(T @event) where T : IEvent
        {
            var topicClient = new TopicClient(_connectionString, @event.CategoryName);

            var eventJson = JsonConvert.SerializeObject(@event);
            var message = new Message(Encoding.UTF8.GetBytes(eventJson))
            {
                ContentType = typeof(T).Name
            };

            //topicClient.SendAsync(new Message(Encoding.UTF8.GetBytes(@event)));
        }
    }
}
