using MicroBootstrap.Commands;
using MicroBootstrap.Events;
using MicroBootstrap.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Services.EventProcessor.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher();
            return serviceCollection;
        }
    }
}