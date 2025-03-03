using BarberApp.Application.Interface;
using BarberApp.Application.Services;
using BarberApp.Persistence;
using BarberApp.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// 🔹 Register Application Services
builder.Services.AddScoped<IBarberRepository, BarberRepository>();
builder.Services.AddScoped<IBarberService, BarberService>();


// 🔹 Register Database Context
builder.Services.AddDbContext<BarberDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
     b => b.MigrationsAssembly("BarberApp.API")));



builder.Services.AddEndpointsApiExplorer();  // This is necessary for Swagger UI to display endpoints
builder.Services.AddSwaggerGen();  // This generates Swagger docs for your API


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
