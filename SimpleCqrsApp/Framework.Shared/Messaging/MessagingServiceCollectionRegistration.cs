using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Shared.Messaging
{
    public static class MessagingServiceCollectionRegistration
    {
        public static void AddMessagingDependencies(this IServiceCollection serviceCollection, string assemblyNameContainingCommandAndHandlers)
        {
            Assembly.Load(assemblyNameContainingCommandAndHandlers);

            serviceCollection
                .AddSingleton<ICommandDispatcher, CommandDispatcher>()
                .AddSingleton<IEventDispatcher, EventDispatcher>()
                .AddSingletonHandlers(typeof(ICommandHandler<>))
                .AddSingletonHandlers(typeof(IEventHandler<>));
        }

        public static IServiceCollection AddSingletonHandlers(this IServiceCollection serviceCollection, Type openGenericType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var concreteCommandHandlerTypes = GetConcreteCommandHandlerTypes(openGenericType, assembly);

                foreach (var concreteCommandHandlerType in concreteCommandHandlerTypes)
                {
                    serviceCollection.AddSingleton(concreteCommandHandlerType.Item1, concreteCommandHandlerType.Item2);
                }
            }

            return serviceCollection;
        }

        private static IEnumerable<Tuple<Type, Type>> GetConcreteCommandHandlerTypes(Type openGenericType, Assembly assembly)
        {
            foreach (var assemblyType in assembly.GetTypes())
            {
                var handlerInterfaces = assemblyType.GetInterfaces().Where(x =>
                    x.IsGenericType && openGenericType == x.GetGenericTypeDefinition());

                foreach (var handlerInterface in handlerInterfaces)
                {
                    yield return new Tuple<Type, Type>(handlerInterface, assemblyType);
                }
            }
        }
    }
}
