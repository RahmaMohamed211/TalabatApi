using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Exentations;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middelwares;
using Talabat.core.Entities;
using Talabat.core.Entities.identity;
using Talabat.core.Repositiories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services with DI
            builder.Services.AddControllers(); //add services ASP Web APIs
            
          
            ////////////////////
            ///*------Database
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection= builder.Configuration.GetConnectionString("Redis");
                return  ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


           
            /////--------------------------------\
            /////--Extentions services
            builder.Services.AddAplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddSwaggerServices();

            #endregion
            var app = builder.Build();

            //clr Explicity 
            #region Update database

            var scope = app.Services.CreateScope(); //Services scoped
            var services = scope.ServiceProvider; //DI
            //LoggerFacotry
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = services.GetRequiredService<StoreContext>(); //Ask clr to create object from store cotext explicity
                await DbContext.Database.MigrateAsync();//update-database
                await StoreCotextSeed.SeedAsync(DbContext);

                var identityDbContext=services.GetRequiredService<AppIdentityDbContext>();
                await identityDbContext.Database.MigrateAsync(); //update-database

                var userManger=services.GetRequiredService<UserManager<AppUser>>();
                await AddIdentityDbContextSeed.SeedUserAsync(userManger);

            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error Occured during apply the Migration ");




            } 
            #endregion








            #region Cofigure request into Piplines

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwaggerMiddlewares();
            }
            app.UseMiddleware<ExceptionMiddelware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");


            app.UseAuthentication();
            app.UseAuthorization();
       
        

            app.MapControllers();

            #endregion
            app.Run();
        }
    }
}