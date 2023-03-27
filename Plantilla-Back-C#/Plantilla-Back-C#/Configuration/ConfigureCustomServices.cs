using PlantillaBack.Services.Email;
using PlantillaBack.Services.Security;
using PlantillaBack.Servicies.Artists;
using PlantillaBack.Servicies.Users;

namespace Plantilla_Back_C_.Configuration
{

    //? En esta parte de la configuraciones irá TODOS los servicios que tengamos en nuestro proyecto, los cuales
    //? Declararemos desde program usando el método AddCustomServices y pasandole el "builder.services"

    internal static class ConfigureCustomServices
    {
        internal static void AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<IArtistsServices, ArtistsServices>();
            services.AddScoped<IUsersServices, UsersServices>();
            services.AddScoped<IEncryptionServices, EncryptionService>();
            services.AddScoped<IEmailService, EmailService>();

        }
    }
}
