using MicroBootstrap.Consul;
using MicroBootstrap.Fabio;
using MicroBootstrap.WebApi;
using MicroBootstrap.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
{

    return serviceCollection
        .AddHttpClient()
        .AddConsul()
        .AddFabio()
        .AddRabbitMq()
        // .AddMongo()
        // .AddRedis()
        // .AddMetrics()
        // .AddJaeger()
        // .AddMongoRepository<DeliveryDocument, Guid>("deliveries")
        // .AddWebApiSwaggerDocs();
}

public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
{
    app.UseErrorHandler()
        .UseSwaggerDocs()
        .UseJaeger()
        .UseConvey()
        .UsePublicContracts<ContractAttribute>()
        .UseMetrics()
        .UseRabbitMq()
        .SubscribeCommand<StartDelivery>()
        .SubscribeCommand<CompleteDelivery>()
        .SubscribeCommand<FailDelivery>()
        .SubscribeCommand<AddDeliveryRegistration>();

    return app;
}