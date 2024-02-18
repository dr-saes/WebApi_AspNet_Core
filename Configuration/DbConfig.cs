using Microsoft.EntityFrameworkCore;

namespace WebApi_AspNet_Core;

public static class DbConfig
{
    public static WebApplicationBuilder AddDbConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 36)));
        });
        return builder;
    }
}
