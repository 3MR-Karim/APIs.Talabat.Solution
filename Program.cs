
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Talabat.Repository.Data;

namespace Talabat.Solution
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
         
            var builder = WebApplication.CreateBuilder(args);

            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var app = builder.Build();

            #region How Clr create opject not implicit DI but Explicit  this beccause need migration then do look Database to apply migration
            using var scoperd = app.Services.CreateScope();
             var Services = scoperd.ServiceProvider;
            var _dbContext= Services.GetRequiredService<StoreContext>();
         var loggerFactory= Services.GetRequiredService<ILoggerFactory>();
            try
            {
            await _dbContext.Database.MigrateAsync();  // update db becuse mifate everseconeed contintu if run app found migration not apply DLR DO Apply
                // and use because data Seeding and if deploy API and cant open PMA for applymigraotn becuase in server  i talk becuase work 

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"an error has been occured during apply the migration");
            }
            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
