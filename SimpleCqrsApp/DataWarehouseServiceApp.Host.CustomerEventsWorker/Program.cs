using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataWarehouseServiceApp.Domain.Events;
using Framework.Shared.Messaging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PolicyAdminServiceApp.Domain.Events;

namespace DataWarehouseServiceApp.Host.CustomerEventsWorker
{
    class Program
    {
        private static EventDispatcher _dispatcher;
        private static bool _isRunning;
        private static SubscriptionClient _subscriptionClient;

        static void Main(string[] args)
        {
            _isRunning = true;

            Console.WriteLine("Started Data Warehouse Events Worker");

            var connectionString = "Endpoint=sb://sb-poc-assad.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=aFc9QxcotbKor/RJTt/nXZKuFGKbz1K1J30ZhmglvXM=";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMessagingDependencies("DataWarehouseServiceApp.Domain");

            _dispatcher = new EventDispatcher(serviceCollection.BuildServiceProvider());
            _subscriptionClient = new SubscriptionClient(connectionString, "customer-success-events", "DataWarehouse");
            RegisterOnMessageHandlerAndReceiveMessages();

            while (_isRunning)
            {
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Policy Admin Customer Events Worker", ex);
                    throw;
                }
            }
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        public static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            IEvent @event;

            if (message.ContentType == typeof(CustomerCreatedEvent).Name)
            {
                @event = JsonConvert.DeserializeObject<CustomerCreatedEvent>(Encoding.UTF8.GetString(message.Body));
            }
            else if (message.ContentType == typeof(CustomerVerifiedAsEligibleEvent).Name)
            {
                @event = JsonConvert.DeserializeObject<CustomerVerifiedAsEligibleEvent>(Encoding.UTF8.GetString(message.Body));
            }
            else
            {
                throw new NotSupportedException($"Event {message.ContentType} is not supported");
            }

            await _dispatcher.DispatchAsync(@event).ConfigureAwait(false);

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken).ConfigureAwait(false);
        }

        public static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");

            // _isRunning = false;

            return Task.CompletedTask;
        }
    }
}
