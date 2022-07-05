using Backend.InhumacionCremacion.BusinessRules;
using Backend.InhumacionCremacion.Entities.Interface.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.InhumacionCremacion.API.Injections
{
    /// <summary>
    ///     Business Injection
    /// </summary>
    public static class BusinessInjection
    {
        /// <summary>
        ///     Adds the business configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddBusinessConfig(this IServiceCollection services)
        {
            services.AddTransient<IRequestBusiness, RequestBusiness>();
            services.AddTransient<IGeneratePDFBusiness, GeneratePDFBusiness>();
            services.AddTransient<ISupportDocumentsBusiness, SupportDocumentsBusiness>();
            services.AddTransient<IUpdateRequestBusiness, UpdateRequestBusiness>();
            services.AddTransient<ISeguimientoBusiness, SeguimientoBusiness>();
            services.AddTransient<IFormatoBusiness, FormatoBusiness>();
           
        }
    }
}