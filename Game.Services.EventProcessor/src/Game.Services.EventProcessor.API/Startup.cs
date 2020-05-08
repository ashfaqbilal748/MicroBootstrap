using System;
using Autofac;
using Common.Consul;
using Common.Dispatchers;
using Common.Jaeger;
using Common.Mongo;
using Common.RabbitMq;
using Common.RestEase;
using Common.WebApi;
using Common;
using Game.Services.EventProcessor.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Swagger;
using Common.Authentication;
using Autofac.Extensions.DependencyInjection;
using Consul;
using Microsoft.Extensions.Hosting;
using Game.Services.EventProcessor.Core.Messages.Commands;
using Common.Redis;
using Microsoft.AspNetCore.Http;

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
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddRedis();
            services.AddJwt();
            services.AddJaeger();
            services.AddOpenTracing();

            services.AddInitializers(typeof(IMongoDbInitializer));
            // services.RegisterServiceForwarder<IOrdersService>("orders-service");
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
            builder.AddDispatchers();
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
            //app.UseAuthorization();
       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Game Event Processor Service");
                });
            });
            
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();
            app.UseRabbitMq();

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                AutofacContainer.Dispose();
            });

        }

    }
}
