using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomerServiceApp.Domain.Commands;
using CustomerServiceApp.Domain.Entities.BrandAggregate;
using CustomerServiceApp.Domain.Entities.CustomerAggregate;
using CustomerServiceApp.Infrastructure.Repositories;
using Framework.Shared.Messaging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CustomerServiceApp.Host.Worker
{
    public class Program
    {
        private static AzureCommandBus _bus;
        private static CommandDispatcher _dispatcher;
        private static bool IsRunning;

        public static void Main(string[] args)
        {
            IsRunning = true;

            Console.WriteLine("Started Customer Worker");

            var connectionString = "Endpoint=sb://sb-poc-assad.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=aFc9QxcotbKor/RJTt/nXZKuFGKbz1K1J30ZhmglvXM=";
            var commandQueueName = "customer-commands";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ICustomerRepository, CustomerSqlRepository>();
            serviceCollection.AddSingleton<IBrandRepository, BrandRestApiRepository>();
            serviceCollection.AddMessagingDependencies("CustomerServiceApp.Domain");

            _dispatcher = new CommandDispatcher(serviceCollection.BuildServiceProvider());
            _bus = new AzureCommandBus(connectionString, commandQueueName);
            GetMessage();

            while (IsRunning)
            {
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Customer Worker", ex);
                    throw;
                }
            }

            Console.WriteLine("Stopping Service");
            Console.ReadLine();
        }

        private static void GetMessage()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _bus.QueueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            try
            {
                await _bus.QueueClient.CompleteAsync(message.SystemProperties.LockToken);

                ICommand command;

                if (message.ContentType == typeof(CreateCustomerCommand).Name)
                {
                    command = JsonConvert.DeserializeObject<CreateCustomerCommand>(
                        Encoding.UTF8.GetString(message.Body));
                }
                else
                {
                    throw new NotSupportedException($"Command {message.ContentType} is not supported");
                }

                await _dispatcher.DispatchAsync(command).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                IsRunning = false;
                Console.WriteLine(e);
                throw;
            }
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
