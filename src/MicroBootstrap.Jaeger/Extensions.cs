using System.Threading;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using MicroBootstrap.Jaeger.Tracers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;

namespace MicroBootstrap.Jaeger
{
    public static class Extensions
    {
        private static int _initialized;
        private const string SectionName = "jaeger";
        private const string RegistryName = "tracing.jaeger";

        public static IServiceCollection AddJaeger(this IServiceCollection serviceCollection, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var options = serviceCollection.GetOptions<JaegerOptions>(sectionName);
            return serviceCollection.AddJaeger(options);
        }

        public static IServiceCollection AddJaeger(this IServiceCollection serviceCollection, JaegerOptions options)
        {
            if (Interlocked.Exchange(ref _initialized, 1) == 1)
            {
                return serviceCollection;
            }

            serviceCollection.AddSingleton(options);
            if (!options.Enabled)
            {
                var defaultTracer = DefaultTracer.Create();
                serviceCollection.AddSingleton(defaultTracer);
                return serviceCollection;
            }

            if (options.ExcludePaths is { })
            {
                serviceCollection.Configure<AspNetCoreDiagnosticOptions>(o =>
                {
                    foreach (var path in options.ExcludePaths)
                    {
                        o.Hosting.IgnorePatterns.Add(x => x.Request.Path == path);
                    }
                });
            }

            serviceCollection.AddOpenTracing();
            serviceCollection.AddSingleton<ITracer>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

                var reporter = new RemoteReporter.Builder()
                    .WithSender(new UdpSender(options.UdpHost, options.UdpPort, options.MaxPacketSize))
                    .WithLoggerFactory(loggerFactory)
                    .Build();

                var sampler = GetSampler(options);

                var tracer = new Tracer.Builder(options.ServiceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithReporter(reporter)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            return serviceCollection;
        }

        public static IApplicationBuilder UseJaeger(this IApplicationBuilder app)
        {
            JaegerOptions options;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                options = scope.ServiceProvider.GetService<JaegerOptions>();
            }

            return app;
        }

        private static ISampler GetSampler(JaegerOptions options)
        {
            switch (options.Sampler)
            {
                case "const": return new ConstSampler(true);
                case "rate": return new RateLimitingSampler(options.MaxTracesPerSecond);
                case "probabilistic": return new ProbabilisticSampler(options.SamplingRate);
                default: return new ConstSampler(true);
            }
        }
    }
}