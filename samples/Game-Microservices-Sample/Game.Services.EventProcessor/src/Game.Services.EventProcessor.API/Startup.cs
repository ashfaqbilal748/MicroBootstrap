using Autofac;
using MicroBootstrap.Consul;
using MicroBootstrap.Jaeger;
using MicroBootstrap.Mongo;
using MicroBootstrap.RabbitMq;
using MicroBootstrap.WebApi;
using MicroBootstrap;
using Game.Services.EventProcessor.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicroBootstrap.Swagger;
using MicroBootstrap.Authentication;
using Autofac.Extensions.DependencyInjection;
using Consul;
using Microsoft.Extensions.Hosting;
using Game.Services.EventProcessor.Core.Messages.Commands;
using MicroBootstrap.Redis;
using Microsoft.AspNetCore.Http;
using Game.Services.EventProcessor.Core.Messages.Events;
using Game.Services.EventProcessor.Application;

namespace Game.Services.EventProcessor.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ILifetimeScope AutofacContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebApi();
            services.AddApplication();
            services.AddHealthChecks();
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddRedis();
            services.AddJwt();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddApplication();
            services.AddInitializers(typeof(IMongoDbInitializer));
            //RestEase Services

        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();
            builder.AddMongo();
            builder.AddMongoRepository<GameEventSource>("GameEventSources");
            builder.AddRabbitMq();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
         IHostApplicationLifetime applicationLifetime, IConsulClient client, IStartupInitializer startupInitializer)
        {
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseInitializers();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                         await context.Response.WriteAsync(context.RequestServices.GetService<AppOptions>().Name));
                endpoints.MapHealthChecks("/healthz");
            });

            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();

            app.UseRabbitMq()
                .SubscribeCommand<AddGameEventSource>(onError: (c, e) =>
                    new AddGameEventSourceRejected(c.Id, e.Message, e.Code));
                // .SubscribeCommand<UpdateGameEventSource>(onError: (c, e) =>
                //     new UpdateGameEventSourceRejected(c.Id, e.Message, e.Code))
                // .SubscribeCommand<DeleteGameEventSource>(onError: (c, e) =>
                //     new DeleteGameEventSourceRejected(c.Id, e.Message, e.Code))

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                AutofacContainer.Dispose();
            });

        }

    }
}
