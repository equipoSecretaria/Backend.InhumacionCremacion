using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Backend.InhumacionCremacion.API.Injections
{
    /// <summary>
    /// </summary>
    public static class InitialInjection
    {
        /// <summary>
        ///     Adds the initial configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddInitialConfig(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        /// <summary>
        ///     Uses the initial configuration.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void UseInitialConfig(this IApplicationBuilder app)
        {
            app.UseCors(x => x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();
        }
    }
}