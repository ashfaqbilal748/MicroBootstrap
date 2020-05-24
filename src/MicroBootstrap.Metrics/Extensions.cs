using System;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.AspNetCore.Endpoints;
using App.Metrics.AspNetCore.Health.Endpoints;
using App.Metrics.AspNetCore.Tracking;
using App.Metrics.Formatters.Prometheus;
using MicroBootstrap.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroBootstrap.Metrics
{
    public static class Extensions
    {
        private static bool _initialized;
        private const string MetricsSectionName = "metrics";
        private const string AppSectionName = "app";
        private const string RegistryName = "metrics.metrics";

        public static IServiceCollection AddAppMetrics(this IServiceCollection serviceCollection,
            string metricsSectionName = MetricsSectionName, string appSectionName = AppSectionName)
        {
            if (string.IsNullOrWhiteSpace(metricsSectionName))
            {
                metricsSectionName = MetricsSectionName;
            }

            if (string.IsNullOrWhiteSpace(appSectionName))
            {
                appSectionName = AppSectionName;
            }

            var metricsOptions = serviceCollection.GetOptions<MetricsOptions>(metricsSectionName);
            var appOptions = serviceCollection.GetOptions<AppOptions>(appSectionName);

            return serviceCollection.AddAppMetrics(metricsOptions, appOptions);
        }

        public static IServiceCollection AddAppMetrics(this IServiceCollection serviceCollection,
            Func<IMetricsOptionsBuilder, IMetricsOptionsBuilder> buildOptions, string appSectionName = AppSectionName)
        {
            if (string.IsNullOrWhiteSpace(appSectionName))
            {
                appSectionName = AppSectionName;
            }

            var metricsOptions = buildOptions(new MetricsOptionsBuilder()).Build();
            var appOptions = serviceCollection.GetOptions<AppOptions>(appSectionName);

            return serviceCollection.AddAppMetrics(metricsOptions, appOptions);
        }

        public static IServiceCollection AddAppMetrics(this IServiceCollection serviceCollection, MetricsOptions metricsOptions,
            AppOptions appOptions)
        {
            serviceCollection.AddSingleton(metricsOptions);
            if (!metricsOptions.Enabled || _initialized)
            {
                return serviceCollection;
            }

            _initialized = true;

            serviceCollection.Configure<KestrelServerOptions>(o => o.AllowSynchronousIO = true);
            serviceCollection.Configure<IISServerOptions>(o => o.AllowSynchronousIO = true);

            var metricsBuilder = new MetricsBuilder().Configuration.Configure(cfg =>
            {
                var tags = metricsOptions.Tags;
                if (tags is null)
                {
                    return;
                }

                tags.TryGetValue("app", out var app);
                tags.TryGetValue("env", out var env);
                tags.TryGetValue("server", out var server);
                cfg.AddAppTag(string.IsNullOrWhiteSpace(app) ? appOptions.Service : app);
                cfg.AddEnvTag(string.IsNullOrWhiteSpace(env) ? null : env);
                cfg.AddServerTag(string.IsNullOrWhiteSpace(server) ? null : server);
                if (!string.IsNullOrWhiteSpace(appOptions.Instance))
                {
                    cfg.GlobalTags.Add("instance", appOptions.Instance);
                }

                if (!string.IsNullOrWhiteSpace(appOptions.Version))
                {
                    cfg.GlobalTags.Add("version", appOptions.Version);
                }

                foreach (var tag in tags)
                {
                    if (cfg.GlobalTags.ContainsKey(tag.Key))
                    {
                        cfg.GlobalTags.Remove(tag.Key);
                    }

                    if (!cfg.GlobalTags.ContainsKey(tag.Key))
                    {
                        cfg.GlobalTags.TryAdd(tag.Key, tag.Value);
                    }
                }
            });

            if (metricsOptions.InfluxEnabled)
            {
                metricsBuilder.Report.ToInfluxDb(o =>
                {
                    o.InfluxDb.Database = metricsOptions.Database;
                    o.InfluxDb.BaseUri = new Uri(metricsOptions.InfluxUrl);
                    o.InfluxDb.CreateDataBaseIfNotExists = true;
                    o.FlushInterval = TimeSpan.FromSeconds(metricsOptions.Interval);
                });
            }

            var metrics = metricsBuilder.Build();
            var metricsWebHostOptions = GetMetricsWebHostOptions(metricsOptions);

            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                serviceCollection.AddHealth();
                serviceCollection.AddHealthEndpoints(configuration);
                serviceCollection.AddMetricsTrackingMiddleware(configuration);
                serviceCollection.AddMetricsEndpoints(configuration);
                serviceCollection.AddSingleton<IStartupFilter>(new DefaultMetricsEndpointsStartupFilter());
                serviceCollection.AddSingleton<IStartupFilter>(new DefaultHealthEndpointsStartupFilter());
                serviceCollection.AddSingleton<IStartupFilter>(new DefaultMetricsTrackingStartupFilter());
                serviceCollection.AddMetricsReportingHostedService(metricsWebHostOptions.UnobservedTaskExceptionHandler);
                serviceCollection.AddMetricsEndpoints(metricsWebHostOptions.EndpointOptions, configuration);
                serviceCollection.AddMetricsTrackingMiddleware(metricsWebHostOptions.TrackingMiddlewareOptions,
                    configuration);
                serviceCollection.AddMetrics(metrics);
            }
            return serviceCollection;
        }

        private static MetricsWebHostOptions GetMetricsWebHostOptions(MetricsOptions metricsOptions)
        {
            var options = new MetricsWebHostOptions();

            if (!metricsOptions.Enabled)
            {
                return options;
            }

            if (!metricsOptions.PrometheusEnabled)
            {
                return options;
            }

            options.EndpointOptions = endpointOptions =>
            {
                switch (metricsOptions.PrometheusFormatter?.ToLowerInvariant() ?? string.Empty)
                {
                    case "protobuf":
                        endpointOptions.MetricsEndpointOutputFormatter =
                            new MetricsPrometheusProtobufOutputFormatter();
                        break;
                    default:
                        endpointOptions.MetricsEndpointOutputFormatter =
                            new MetricsPrometheusTextOutputFormatter();
                        break;
                }
            };

            return options;
        }

        public static IApplicationBuilder UseAppMetrics(this IApplicationBuilder app)
        {
            MetricsOptions options;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                options = scope.ServiceProvider.GetService<MetricsOptions>();
            }

            return !options.Enabled
                ? app
                : app.UseHealthAllEndpoints()
                    .UseMetricsAllEndpoints()
                    .UseMetricsAllMiddleware();
        }
    }
}