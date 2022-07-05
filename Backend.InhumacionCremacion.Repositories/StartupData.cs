using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Backend.InhumacionCremacion
{
    /// <summary>
    /// Startup Data.
    /// </summary>
    public static class StartupData
    {
        /// <summary>
        /// The timeout seconds
        /// </summary>
        private const int TIMEOUT_SECONDS = 60;

        /// <summary>
        /// Configure data.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <param name="connectionString">Connection string.</param>
        public static void AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Repositories.Context.InhumacionCremacionContext>(options =>
            {
                options.UseSqlServer(configuration.GetValue<string>(Utilities.Constants.KeyVault.SQLDBInhumacionCremacion),
                    sqlOptions =>
                    {
                        sqlOptions.CommandTimeout(TIMEOUT_SECONDS);
                        

                    });
            });

            services.AddDbContext<Repositories.Context.CommonsContext>(options =>
            {
                options.UseSqlServer(configuration.GetValue<string>(Utilities.Constants.KeyVault.SQLDBCommons),
                    sqlOptions =>
                    {
                        sqlOptions.CommandTimeout(TIMEOUT_SECONDS);

                    });
            });
            services.AddScoped(typeof(Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<>), typeof(Repositories.Base.BaseRepositoryInhumacionCremacion<>));
            services.AddScoped(typeof(Entities.Interface.Repository.IBaseRepositoryCommons<>), typeof(Repositories.Base.BaseRepositoryCommons<>));
            services.AddSingleton<Repositories.Context.OracleContext>();
        }
    }
}
