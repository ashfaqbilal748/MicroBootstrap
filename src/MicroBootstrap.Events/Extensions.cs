using System;
using MicroBootstrap.Events.Dispatchers;
using Microsoft.Extensions.DependencyInjection;
namespace MicroBootstrap.Events
{
    public static class Extensions
    {
        public static IServiceCollection AddInMemoryEventDispatcher(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IEventDispatcher, InMemoryEventDispatcher>();
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(s =>
            s.FromAssemblies(System.Reflection.Assembly.GetCallingAssembly())
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            return serviceCollection;
        }
    }
}