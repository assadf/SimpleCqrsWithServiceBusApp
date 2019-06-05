using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Shared.Messaging
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            var concreteCommandType = command.GetType();
            var closedGenericType = typeof(ICommandHandler<>).MakeGenericType(concreteCommandType);

            var handler = _serviceProvider.GetService(closedGenericType);

            await (Task)handler
                .GetType()
                .GetTypeInfo()
                .GetDeclaredMethod("HandleAsync")
                .Invoke(handler, new object[] { command });
        }
    }
}
