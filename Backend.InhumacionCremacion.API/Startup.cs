using Backend.InhumacionCremacion.API.Injections;
using Backend.InhumacionCremacion.Utilities;
using Backend.InhumacionCremacion.Utilities.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wkhtmltopdf.NetCore;

namespace Backend.InhumacionCremacion.API
{
    /// <summary>
    ///     Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <value>
        ///     The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddWkhtmltopdf("Tools");
            services.AddInitialConfig();
            services.AddData(Configuration);
            services.AddUtilities(Configuration.GetValue<string>(KeyVault.InstrumentationKey));
            services.AddBusinessConfig();
            services.AddSwaggerConfig();
            services.AddCors();
        }

        /// <summary>
        ///     Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseInitialConfig();
            app.UseSwaggerConfig();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:3000/"));
            //app.UseRouting();
            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}