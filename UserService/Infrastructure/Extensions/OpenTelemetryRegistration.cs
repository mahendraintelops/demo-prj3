using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using OpenTelemetry.Exporter;

namespace Infrastructure.Extensions
{
    public static class OpenTelemetryRegistration
    {
        public static IServiceCollection AddCustomOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
             var serviceName = configuration["Otel:ServiceName"];
             var otlpEndpoint = configuration["Otel:OtlpEndpoint"];
             services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                    resource.AddService(serviceName: serviceName))
                .WithMetrics(metrics => metrics
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter(otlpOptions =>
                    {
                            otlpOptions.Endpoint = new Uri(otlpEndpoint);
                            otlpOptions.Protocol = OtlpExportProtocol.Grpc;
                    }))
                .WithTracing(tracing => tracing
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter(otlpOptions =>
                    {
                             otlpOptions.Endpoint = new Uri(otlpEndpoint);
                             otlpOptions.Protocol = OtlpExportProtocol.Grpc;
                    })
                     .AddConsoleExporter());
           // Register Tracer so it can be injected into other components (eg Controllers)
            services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));

            return services;
        }

        public static ILoggingBuilder AddCustomOpenTelemetryLogging(this ILoggingBuilder builder, IConfiguration configuration)
        {
            var serviceName = configuration["Otel:ServiceName"];
            return builder.AddOpenTelemetry(options =>
            {
                options
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                            .AddService(serviceName));
            });
        }
    }
}
