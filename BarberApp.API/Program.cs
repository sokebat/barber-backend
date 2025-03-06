using BarberApp.Application.Interface;
using BarberApp.Application.Services;
using BarberApp.Persistence;
using BarberApp.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);


Batteries.Init();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// 🔹 Register Application Services
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IOurServicesRepository, OurServicesRepository>();
builder.Services.AddScoped<IOurServicesService, OurServicesService>();


// 🔹 Register Database Context for SQLite
builder.Services.AddDbContext<BarberDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
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
