using System;
using Backend.InhumacionCremacion.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Backend.InhumacionCremacion.API
{
    /// <summary>
    ///     Program
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddAzureKeyVault(
                    $"https://{Environment.GetEnvironmentVariable(Utilities.Constants.Environment.KeyVaultUrl).ToStringBase64()}.vault.azure.net/",
                    Environment.GetEnvironmentVariable(Utilities.Constants.Environment.ClientId).ToStringBase64(),
                    Environment.GetEnvironmentVariable(Utilities.Constants.Environment.ClientSecret).ToStringBase64());
            }).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}