###############################################################################
###############################################################################

				PASOS A SEGUIR PARA CREAR TU BACK EN C#

###############################################################################
###############################################################################

1.- Creamos nuestro proyecto API.

2.- Creamos nuestra estructura de proyectos (Entities, Repositories, Servicies).

3.- Creamos las Clases de nuestro Entities, las cuales en un futuro será nuestras tablas de BBDD.
	(Por convenio el nombre de estas clases será en singular)
	
4.- En nuestras Entities necesitaremos verificar la información que se envia y se devuelve a la BBDD
	Para ello crearemos en cada Carpeta de nuestras Entities, una carpeta Request donde controlaremos
	el contenido que se le envia a la BBDD y en aquellas Entities que necesitemos recibir información
	crearemos una carpeta Response que controle la información que contiene dicha respuesta.

	Para trabajar con este método usaremos un NuGet en nuestras Entities que controle el contenido de las 
	clases. Este NuGet es FluentValidation.

	Documentación FluentValidation: https://docs.fluentvalidation.net/en/latest/ 
	
5.- Creamos las Clases de nuestro Services, las cuales actuaran como controladores de nuestras peticiones
	a la BBDD. En nuestro ejemplo realizaremos un CRUD. Siempre nos devuelve un TASK debido a que 
	trabajos con una API y esta es asíncrona, es por ello que necesitamos hacer peticiones asíncronas.

6.- Antes de crear las funcionalidades de nuestro Services, vamos a crear la estructura de nuestro
	Repositories. Una vez creada nuestra estructura BÁSICA, nos dirigimos a nuestro program para 
	estructurar la configuración de nuestra API. Vamos a Refactorizar todas las configuraciones en una 
	carpeta llamada "Configuration".

7.- En nuestro proyecto Repository, creamos nuestra clase "DataBaseContext" donde espeficiamos cómo queremos
	que se realice nuestras conexión a la BBDD y que estructura va a contener. Una vez creada nuestra clase
	"DataBaseContext" tendremos que ir a nuestra carpeta de "Configuration" y crear una clase 
	"ConfigureCustomDBContext" donde realicemos la conexión de nuestro program con la base de Datos.

	7.1.- Cuando vayamos a realizar nuestra "Migración" con la base de datos y queramos evitar tener que
	"comentar" nuestro constructor de "DataBaseContext" podemos indicar que la conexión a la BBDD la
	realice desde nuestro "Program", para ello usaremos un comando "Dotnet" nuevo.

	dotnet ef --startup-project DIRECCIÓN_DEL_PROGRAM migration add NOMBRE_DE_LA_MIGRACIÓN

	7.2.- Cuando vayamos a realizar una ACTUALIZACIÓN de nuestra migraciones como anteriormente le dijimos
	que la conexión a base de datos la realizara desde el "Program", ahora también necesitamos indicarle
	que la ACTUALIZACIÓN se conecte desde el "Program" con el siguiente comando "Dotnet"

	dotnet ef --startup-project DIRECCIÓN_DEL_PROGRAM database update

8.- Una vez que tengamos nuestra BBDD y nuestra conexión a ella. Nos dirigimos a nuestro Repository a 
	crear las peticiones CRUD a ella.

9.- Una vez que tengamos nuestras peticiones de "REPOSITORY" realizadas, nos dirigimos a nuestros 
	"Services" a realizar nuestro "CONTROL" de peticiones. (Como hemos explicado alguna vez, vamos
	a contruir nuestro peaje).
	
	9.1.- En este ejemplo que estamos realizando como plantilla, en el "Users" vamos a encriptar las
	Contraseñas de los "Users" para ello crearemos un "Servicies" que nos realice un método de 
	"Encriptación" y otro método de "Desencriptación" utilizando las propiedades de Microsoft.
	
		9.1.1.- En este momento, necesitaremos ir a nuestro archivo "appsettings" y añadir una "key"
		de encriptamiento, la cual usará el servicio que acabamos de crear para encriptar las contraseñas.

	9.2.-Como en este ejemplo en el "Users" vamos a devolver desde nuestra	BBDD un "Token", para ello 
	antes de realizar el "Services" necesitaremos ir a nuestra "Entities" de "User" y realizar una 
	respuesta que contenga el "Token" en la clase, en nuestro caso lo llamaremos "AuthenticateResponse".
		
		9.2.1.- En este momento, necesitaremos ir a nuestro archivo "appsettings" y añadir la configuración
		de "JWT" el cual nos generará el token para nuestros usuarios.

		9.2.2.- Ahora tocaria ir a nuestra carpeta "Configuration" y crear una inyección de dependencia
		de nuestro "Authentication" donde añadamos nuestras autenticaciones y el Token creado al "Program"
		(De agradecer que Cesar verifique esto). Necesitaremos usar el NuGet "Microsoft AspNetCore 
		Authentication JwtBearer", algo a tener en cuenta durante la instalación es que este NuGet 
		se tiene que instalar la versión correspondiente a tu vesión de NET (en nuestro caso NET 6)


10.- Una vez que tengamos nuestros "Servicies" creado y con ello un control de nuestras peticiones, vamos
	a realizar el "Middlewares" para tener un control de la "Excepciones", de esta manera podremos controlar
	todos los errores que surgan durante nuestras peticiones a la API. Este "Middlewares" lo realizaremos
	en el proyecto principal de la API. Para ello, justo antes de crear esta clase deberemos ir a 
	"Entities" y crearnos una Clase Entidad para los errores.
	
	10.1.-En el middleware cuando declaremos la propieda "VaidationException" y vayamos a hacer 
		"CTRL + PUNTO" debemos tener extremado cuidado de no implementar la librería que no es, ya que
		este método lo tiene tanto el NuGet "FluentValidation" como "Microsoft". NOSOTROS usaremos el
		NuGet "FluentValidation". 

	
11.- En este punto ya podemos crearnos unos datos iniciales para nuestra BBDD, para ello creamos en 
	nuestra carpeta de "Configuration" una clase que nos permita hacer lo que deseamos, en esta plantilla
	la hemos llamado "ConfigureCustomInitDataBase", y ella hemos definido unos datos iniciales para la hora 
	de arrancar nuestra app. Una vez, tengamos la clase realizada debemos inyectarla al "Program"

12.- A estas alturas podemos realizar los Controllers con las peticiones API que deseemos.