
using Microsoft.EntityFrameworkCore;
using TransactionAPI.DatabaseContext;
using TransactionAPI.Models.Repository;

namespace TransactionAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDatabaseContext>(
                opt =>
                {
                    string connString = builder.Configuration.GetConnectionString("DefConn");

                    opt.UseSqlServer(connString, sqlServerOptionsAction: sqlopt =>
                    {
                        sqlopt.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                });

            builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
