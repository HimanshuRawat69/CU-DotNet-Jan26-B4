using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartBank.AccountService.Data;
using SmartBank.AccountService.Exceptions;
using SmartBank.AccountService.Repositories;
using SmartBank.AccountService.Services;
using System.Text;

namespace SmartBank.AccountService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AccountDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService,
                    SmartBank.AccountService.Services.AccountService>();

            // JWT Service
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "SmartBank",
                    ValidAudience = "SmartBankUsers",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("THIS_IS_SUPER_SECRET_KEY_12345_ForMyBankingApp"))
                };
            });

            // added to call transaction microservice - register typed HttpClient for TransactionApiClient
            builder.Services.AddHttpClient<ITransactionApiClient, TransactionApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7185/"); // TransactionService URL
            });

            builder.Services.AddAuthorization();
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

            app.UseAuthentication();

            // Correct Order
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
