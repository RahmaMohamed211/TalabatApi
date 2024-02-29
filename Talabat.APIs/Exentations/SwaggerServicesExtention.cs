using System.Runtime.CompilerServices;

namespace Talabat.APIs.Exentations
{
    public static class SwaggerServicesExtention
    {
        public  static IServiceCollection AddSwaggerServices( this IServiceCollection Services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           Services.AddEndpointsApiExplorer();
           Services.AddSwaggerGen();
            return Services;
        }
        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;

        }
    }
}
