using FluentValidation;
using PlantillaBack.Entities.CommonResponses;
using System.Net;

namespace Plantilla_Back_C_.Middlewares
{
    public class ExceptionHandlingMiddleware
    {

        //? Declaramos las librerias "RequestDelegate" y "ILogger" que son pripias de C#.

        public readonly RequestDelegate _requestDelegate;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.ToString());
            var errorMessageObject = new { Message = ex.Message, Code = "system_error", InnerException = ex.InnerException?.ToString() };
            var errorMessage = System.Text.Json.JsonSerializer.Serialize(errorMessageObject);

            context.Response.ContentType = "application/json";

            if (ex is ValidationException)
            {
                var castException = ex as ValidationException;
                var listErrors = new List<ErrorResponse>();
                foreach (var error in castException.Errors)
                {
                    listErrors.Add(new ErrorResponse() { ErrorDescription = error.ErrorMessage });
                }

                errorMessage = System.Text.Json.JsonSerializer.Serialize(listErrors);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            if (ex is InvalidDataException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
