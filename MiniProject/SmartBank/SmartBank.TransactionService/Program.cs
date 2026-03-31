using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartBank.TransactionService.Data;
using SmartBank.TransactionService.Repositories;
using SmartBank.TransactionService.Services;

namespace SmartBank.TransactionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SmartBankTransactionServiceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SmartBankTransactionServiceContext") ?? throw new InvalidOperationException("Connection string 'SmartBankTransactionServiceContext' not found.")));

            // Add services to the container.
            builder.Services.AddScoped<ITransactionService, TransactionService1>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
