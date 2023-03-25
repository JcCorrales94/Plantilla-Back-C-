using Microsoft.EntityFrameworkCore;
using PlantillaBack.Entities.Roles;
using PlantillaBack.Entities.Users;
using PlantillaBack.Repositories;
using PlantillaBack.Services.Security;

namespace Plantilla_Back_C_.Configuration
{
    internal static class ConfigureCustomInitDataBase
    {
        internal static async Task Init(WebApplication app)
        {

            //? INICIAMOS ENLACE CON NUESTRO SERVIDOR
            using (var scope = app.Services.CreateScope())
            {
                //? LE PEDIMOS QUE REALICE UN ENLACE
                var services = scope.ServiceProvider;

                //? LE INDICAMOS QUE EL ENLACE SEA AL SERVIDOR DEFINIDO EN DATABASECONTEXT
                var dbContext = services.GetRequiredService<DataBaseContext>();

                //? DEFINIMOS QUE CADA VEZ QUE HAYA CAMBIO, NOS REALICE UNA MIGRACIÓN
                dbContext.Database.Migrate();

                //? COMPROBAMOS SI EXISTEN ROLES EN NUESTRA TABLA DE ROL DE LA BBDD
                var existingRoles = await dbContext.Roles.ToListAsync();
                var encryptionService = services.GetRequiredService<IEncryptionServices>();

                if(!existingRoles.Any())
                {
                    await dbContext.Roles.AddAsync(new Rol { Description = "Admin" });
                    await dbContext.Roles.AddAsync(new Rol { Description = "Usuario" });
                    await dbContext.SaveChangesAsync();
                }

                var existingAdmin = await dbContext.Users.Where(x => x.RolId == 1).FirstOrDefaultAsync();
                if(existingAdmin == null) 
                {
                    await dbContext.Users.AddAsync(new User
                    {
                        Name = "default-admin",
                        Email = "default-admin@admin.com",
                        RolId = 1,
                        Password = encryptionService.EncryptString("123456aA.")
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
