using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Plantilla_Back_C_.Configuration
{
    internal static class ConfigureCustomAuthentication
    {
        internal static void AddConfigureCustomAuthentication (IServiceCollection services, ConfigurationManager configuration)
        {
            // ADDING AUTHENTICATION // AQUÍ INDICAMOS QUE AUTENTIFICACIÓN VAMOS A USAR 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // ADDING JWT Bearer // AQUÍ INDICAMOS CON QUE VALORES SE VA A FIRMAR EL TOKEN
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"])),
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                };
            });
        }
    }
}
