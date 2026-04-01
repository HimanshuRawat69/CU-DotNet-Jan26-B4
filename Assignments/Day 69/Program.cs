using Microsoft.EntityFrameworkCore;
using Day69.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connString = Environment.GetEnvironmentVariable("SCAFFOLD_CONN_STR");


// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Enable Content Negotiation
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
})
.AddXmlSerializerFormatters(); // Adds XML support


// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();