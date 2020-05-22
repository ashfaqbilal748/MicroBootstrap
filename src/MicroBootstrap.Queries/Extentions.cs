using System;
using MicroBootstrap.Queries.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace MicroBootstrap.Queries
{
    public static class Extensions
    {
        public static IServiceCollection AddQueryHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            return serviceCollection;
        }
        
        public static IServiceCollection AddInMemoryQueryDispatcher(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
        }
    }
}
