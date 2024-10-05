namespace UsingFirebase.Configurations;

public static class AppConfiguration
{
    public static void ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Versão 1.0");
                x.InjectStylesheet("/css/swaggerDark.css");
                x.RoutePrefix = string.Empty;
            });
        }

        app.UseStaticFiles();

        app.UseCors("AllowAllOrigins");

        app.UseHttpsRedirection();

        app.UseAuthorization();
    }
}