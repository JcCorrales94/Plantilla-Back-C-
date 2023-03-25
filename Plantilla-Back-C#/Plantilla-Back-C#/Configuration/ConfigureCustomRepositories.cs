using PlantillaBack.Repositories.Artists;
using PlantillaBack.Repositories.Roles;
using PlantillaBack.Repositories.Users;

namespace Plantilla_Back_C_.Configuration
{
    internal static class ConfigureCustomRepositories
    {
        internal static void AddCustomRepositories(IServiceCollection services)
        {
            services.AddScoped<IArtistsRepository, ArtistsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
        }
    }
}
