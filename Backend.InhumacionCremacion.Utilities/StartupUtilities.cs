using Microsoft.Extensions.DependencyInjection;

namespace Backend.InhumacionCremacion.Utilities
{
    public static class StartupUtilities
    {
        public static void AddUtilities(this IServiceCollection services, string instrumentationKey)
        {
            services.AddApplicationInsightsTelemetry(instrumentationKey);
            services.AddTransient<Utilities.Telemetry.ITelemetryException, Utilities.Telemetry.TelemetryException>();
            services.AddSingleton<Microsoft.ApplicationInsights.Extensibility.ITelemetryInitializer, Utilities.Telemetry.TelemetryInitializer>();
        }
    }
}
