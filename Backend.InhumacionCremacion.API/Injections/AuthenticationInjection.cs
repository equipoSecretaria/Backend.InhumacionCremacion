using Backend.I2v2.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.InhumacionCremacion.API.Injections
{
    /// <summary>
    /// Authentication Injection
    /// </summary>
    public static class AuthenticationInjection
    {
        /// <summary>
        /// Adds the authentication configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="secretkey">The secretkey.</param>
        public static void AddAuthenticationConfig(this IServiceCollection services, string secretkey)
        {
            var encodedKey = Encoding.ASCII.GetBytes(secretkey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(encodedKey),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
               x.Events = new JwtBearerEvents
               {
                   OnChallenge = context =>
                   {
                       context.Response.OnStarting(async () =>
                       {
                           context.Response.ContentType = "application/json";
                           await context.Response.WriteAsync((new Entities.Responses.ResponseBase<bool>(System.Net.HttpStatusCode.Unauthorized, "La sesión ha caducado!")).Serialize());
                       });

                       return System.Threading.Tasks.Task.CompletedTask;
                   }
               };
           });
        }
    }
}
