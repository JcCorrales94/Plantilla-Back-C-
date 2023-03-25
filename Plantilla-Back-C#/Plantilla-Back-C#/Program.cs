using Plantilla_Back_C_.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//TODO ============================== SWAGGER ==========================================================

//? CONFIGURACIÓN SWAGGERGEN
//La inyeccion del Swagger se encuentra en Configure/ConfigureCustomSwagger
ConfigureCustomSwagger.AddCustomSwagger(builder.Services);

//TODO ============================= INYECCIÓN DE DEPENDENCIAS =========================================

//? INYECCIÓN DE DEPENDECIA DE DATABASECONTEXT
// La inyección del DataBaseContext se encuentra en Configuration/ConfigureCustomDBContext
ConfigureCustomDBContext.AddConfigureCustomDBContext(builder.Services, builder.Configuration);

//? INYECCIÓN DE DEPENDENCIA DE LOS SERVICIOS
// La inyección de los Services se encuentra en Configuration/ConfigureCustomServices
ConfigureCustomServices.AddCustomServices(builder.Services);

//? INYECCIÓN DE DEPENDENCIA DE LOS REPOSITORIOS
// La inyección de los Repository se encuentra en Configuration/ContugreCustomRepositories
ConfigureCustomRepositories.AddCustomRepositories(builder.Services);

//TODO ============================= AUTHENTICATION ====================================================

//? INYECCIÓN DEL AUTHENTICATION JWT
// La inyeccion de la authenticación se encuentra en Configuration/ConfigureCustomAuthentication
ConfigureCustomAuthentication.AddConfigureCustomAuthentication(builder.Services, builder.Configuration);


//? EL APP = BUILDER.BUILD() DEBE IR ENTRE LA AUTENTIFICACIÓN DEL TOKEN Y LA INYECCIÓN INICIAL DE BASE DE DATOS
var app = builder.Build();



//TODO ============================= INICIALIZADOR DDBB =================================================
//? INYECIÓN DEL INICIALIZADOR DE LA BASE DE DATO Y SU CONFIGURACIÓN INICIAL
// La inyección del inicializador se encuentra en Configuration/ConfigureCustomDBContext

await ConfigureCustomInitDataBase.Init(app);


//TODO ============================= CONFIGURE THE HTTP REQUEST =========================================
//? INYECCIÓN DE LA CONFIGURACIÓN DEL HTTP REQUEST
// La inyección de la configuracion HTTP se encuentra en Configuration/ConfigurationCustomApp


ConfigureCustomApp.Configure(app);


app.Run();
