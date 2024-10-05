using UsingFirebase.Configurations;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureService(builder.Configuration);


builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8080);
    serverOptions.ListenAnyIP(8081);
});

var app = builder.Build();

app.ConfigureApp(app.Environment);

app.MapControllers();

app.Run();