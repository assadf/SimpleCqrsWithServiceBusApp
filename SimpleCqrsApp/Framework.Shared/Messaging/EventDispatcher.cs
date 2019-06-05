using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Framework.Shared.Messaging
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<T>(T @event) where T : IEvent
        {
            var concreteCommandType = @event.GetType();
            var closedGenericType = typeof(IEventHandler<>).MakeGenericType(concreteCommandType);

            var handler = _serviceProvider.GetService(closedGenericType);

            if (handler == null)
            {
                Console.WriteLine($"{nameof(handler)} is not defined");
                throw new NullReferenceException($"{nameof(handler)} is not defined");
            }

            await (Task)handler
                .GetType()
                .GetTypeInfo()
                .GetDeclaredMethod("HandleAsync")
                .Invoke(handler, new object[] { @event });
        }
    }
}
