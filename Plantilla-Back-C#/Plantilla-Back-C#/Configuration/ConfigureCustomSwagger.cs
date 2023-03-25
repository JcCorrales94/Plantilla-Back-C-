using Microsoft.OpenApi.Models;

namespace Plantilla_Back_C_.Configuration
{
    internal static class ConfigureCustomSwagger
    {
        internal static void  AddCustomSwagger(IServiceCollection services)
        {
            //? "builder.Services" es de tipado "IServiceCollection" Como estamos refactorizando, nosotros en el
            //? program necesitaremos pasarle al metodo "AddCustomSwagger" el "builder.Services".

            //? El Swagger no es otra cosa que la interfaz que usamos al levantar el proyecto y simular 
            //? peticiones a la API.


            //? La estructura que podemos observar en el método "AddSwaggerGen" es el que nos indica que debemos
            //? usar SWAGGER en su documentación, por lo que será algo reutilizable en otros proyectos.


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(cfg =>
            {
                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                       },
                       new List<string>()
                    }
                });
            });
        }
    }
}
