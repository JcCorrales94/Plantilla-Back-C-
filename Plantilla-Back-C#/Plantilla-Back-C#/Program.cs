using Plantilla_Back_C_.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//TODO ============================== SWAGGER ==========================================================

//? CONFIGURACI�N SWAGGERGEN
//La inyeccion del Swagger se encuentra en Configure/ConfigureCustomSwagger
ConfigureCustomSwagger.AddCustomSwagger(builder.Services);

//TODO ============================= INYECCI�N DE DEPENDENCIAS =========================================

//? INYECCI�N DE DEPENDECIA DE DATABASECONTEXT
// La inyecci�n del DataBaseContext se encuentra en Configuration/ConfigureCustomDBContext
ConfigureCustomDBContext.AddConfigureCustomDBContext(builder.Services, builder.Configuration);

//? INYECCI�N DE DEPENDENCIA DE LOS SERVICIOS
// La inyecci�n de los Services se encuentra en Configuration/ConfigureCustomServices
ConfigureCustomServices.AddCustomServices(builder.Services);

//? INYECCI�N DE DEPENDENCIA DE LOS REPOSITORIOS
// La inyecci�n de los Repository se encuentra en Configuration/ContugreCustomRepositories
ConfigureCustomRepositories.AddCustomRepositories(builder.Services);

//TODO ============================= AUTHENTICATION ====================================================

//? INYECCI�N DEL AUTHENTICATION JWT
// La inyeccion de la authenticaci�n se encuentra en Configuration/ConfigureCustomAuthentication
ConfigureCustomAuthentication.AddConfigureCustomAuthentication(builder.Services, builder.Configuration);


//? EL APP = BUILDER.BUILD() DEBE IR ENTRE LA AUTENTIFICACI�N DEL TOKEN Y LA INYECCI�N INICIAL DE BASE DE DATOS
var app = builder.Build();



//TODO ============================= INICIALIZADOR DDBB =================================================
//? INYECI�N DEL INICIALIZADOR DE LA BASE DE DATO Y SU CONFIGURACI�N INICIAL
// La inyecci�n del inicializador se encuentra en Configuration/ConfigureCustomDBContext

await ConfigureCustomInitDataBase.Init(app);


//TODO ============================= CONFIGURE THE HTTP REQUEST =========================================
//? INYECCI�N DE LA CONFIGURACI�N DEL HTTP REQUEST
// La inyecci�n de la configuracion HTTP se encuentra en Configuration/ConfigurationCustomApp


ConfigureCustomApp.Configure(app);


app.Run();
