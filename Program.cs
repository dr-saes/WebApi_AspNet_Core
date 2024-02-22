using WebApi_AspNet_Core;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
//builder.Services.AddControllersWithViews();

builder
    .AddBasicConfig()
    .AddSwaggerConfig()
    .AddDbConfig()
    .AddIdentityConfig();

var app = builder.Build();
app.AddWebAppConfig();

app.Run();
