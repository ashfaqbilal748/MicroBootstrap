using System;
using Game.Services.Messaging.Application.Services;
using Game.Services.Messaging.Core.Services;
using MicroBootstrap.Commands;
using MicroBootstrap.Events;
using MicroBootstrap.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Services.Messaging.Application
{
    public static class Extensions
    {
        public static string ToUserGroup(this Guid userId)
            => $"users:{userId}";

        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher();

            serviceCollection.AddTransient<IHubService, HubService>();
            serviceCollection.AddTransient<IHubWrapper, HubWrapper>();
            return serviceCollection;
        }
    }
}