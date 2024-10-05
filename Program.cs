using UsingFirebase.Configurations;
using DotNetEnv;

namespace UsingFirebase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(8080);
                serverOptions.ListenAnyIP(8081);
            });
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.ConfigureApp(app.Environment);

            app.MapControllers();

            app.Run();
        }
    }
}