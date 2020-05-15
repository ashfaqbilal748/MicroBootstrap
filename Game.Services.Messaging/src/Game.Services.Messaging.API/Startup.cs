using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Common.Authentication;
using Common.Consul;
using Common.Dispatchers;
using Common.Jaeger;
using Common.Mongo;
using Common.RabbitMq;
using Common.Redis;
using Common.Swagger;
using Common.WebApi;
using Consul;
using Game.Services.Messaging.Application.Services;
using Game.Services.Messaging.Core.Entities;
using Game.Services.Messaging.Core.Messages.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Game.Services.Messaging.API
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
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();
            services.AddJwt();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });
            AddSignalR(services);
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
            builder.AddDispatchers();
            builder.AddMongo();
            builder.AddMongoRepository<GameEventSource>("GameEventSources");
            builder.AddRabbitMq();
        }

        private void AddSignalR(IServiceCollection services)
        {
            var options = Configuration.GetOptions<SignalrOptions>("signalr");
            services.AddSingleton(options);
            var builder = services.AddSignalR();
            if (!options.Backplane.Equals("redis", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }
            var redisOptions = Configuration.GetOptions<RedisOptions>("redis");
            builder.AddRedis(redisOptions.ConnectionString);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
          IHostApplicationLifetime applicationLifetime, IConsulClient client,
          IStartupInitializer startupInitializer, SignalrOptions signalrOptions)
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
                endpoints.MapGet("/", async context =>
                            {
                                await context.Response.WriteAsync("Game Messaging Service");
                            });
                endpoints.MapHub<GameHub>("/gameHub");
            });
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();

            app.UseRabbitMq()
            .SubscribeEvent<GameEventSourceAdded>(@namespace: "game-event-sources");

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                AutofacContainer.Dispose();
            });

        }
    }
}
