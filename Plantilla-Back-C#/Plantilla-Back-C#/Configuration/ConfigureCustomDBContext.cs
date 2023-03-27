using Microsoft.EntityFrameworkCore;
using PlantillaBack.Repositories;

namespace Plantilla_Back_C_.Configuration
{
    internal static class ConfigureCustomDBContext
    {
        internal static void AddConfigureCustomDBContext(IServiceCollection services, ConfigurationManager configuration)
        {

        //? La cadena de conexión a nuestra BBDD.

            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
        }
    }
}
