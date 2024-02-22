using Microsoft.AspNetCore.Mvc;

namespace WebApi_AspNet_Core;

public static class BasicConfig
{
    public static WebApplicationBuilder AddBasicConfig(this WebApplicationBuilder builder)
    {
        //Environments
        builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

        //Cors
        builder.Services.AddCors(option =>
        {
            option.AddPolicy("Development", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            option.AddPolicy("Production", builder =>
                builder
                     .AllowAnyOrigin()
                     //.WithOrigins("http://localhost:9000")
                     .AllowAnyMethod()
                    //.WithMethods("POST")
                    .AllowAnyHeader());
        });
        //CSRF
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        });

        return builder;



    }

    public static WebApplication AddWebAppConfig(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //forçar https.
            app.UseHsts();
            app.UseCors("Development");
        }
        else
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //forçar https.
            app.UseHsts();
            app.UseCors("Production");
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;

    }
}
