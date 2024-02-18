namespace WebApi_AspNet_Core;

public static class CorsConfig
{
    public static WebApplicationBuilder AddCorsConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(option =>
        {
            option.AddPolicy("Development", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            option.AddPolicy("Production", builder =>
                builder
                .WithOrigins("http://localhost:9000")
                .WithMethods("POST")
                .AllowAnyHeader());
        });
        return builder;
    }
}
