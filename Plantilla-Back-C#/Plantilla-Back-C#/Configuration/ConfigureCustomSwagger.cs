using Microsoft.OpenApi.Models;
using System.Reflection;

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

            //? Esto nos permite tener en nuestro Swagger un controlador de autentificación para lanzar peticiones
            //? con el token
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
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Plantilla De Un Back en C#",
                    Description = "Plantilla de clase para ayudarnos a construir un back de C#"

                });
                // SET THE COMMENTS PATH FOR THE SWAGGER JSON AND UI
                //? PARA QUE ESTO FUNCIONE NECESITAMOS EN NUESTRA API HACE CLICK IZQUIERDO, PROPIEDADES Y BUSCAR
                //? DOCUMENTATION Y MARCAR LA CASILLA QUE NOS SALDRÁ QUE GENERARÁ UN ARCHIVO SOBRE LOS COMENTARIOS
                //? QUE TENGAMOS EN NUESTRO CÓDIGO. ESTOS COMENTARIÓS SON AQUELLOS QUE SE FORMAN CON 3 /. COMO EL 
                //? EJEMPLO QUE PODEMOS ENCONTRAR EN EL "ARTISTcONTROLLER".

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });

        }
    }
}
