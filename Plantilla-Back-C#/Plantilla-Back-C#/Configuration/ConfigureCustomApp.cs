namespace Plantilla_Back_C_.Configuration
{
    internal static class ConfigureCustomApp
    {
        internal static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //? Necesitamos añadir el "UseAuthentication" para que funcione todo el tema de Autenticaciones

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
