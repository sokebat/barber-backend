using BarberApp.Application.Interface;
using BarberApp.Application.Services;
using BarberApp.Domain;
using BarberApp.Persistence;
using BarberApp.Persistence.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        Batteries.Init();
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        // 🔹 Register Database Context for SQLite
        //builder.Services.AddDbContext<BarberDbContext>(options =>
        //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        //     b => b.MigrationsAssembly("BarberApp.API")));
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<BarberDbContext>(options =>
          options.UseSqlite(connectionString));

       builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<BarberDbContext>()
            .AddDefaultTokenProviders();

        // 🔹 Configure JWT Authentication
        var jwtKey = builder.Configuration["Jwt:Key"];
        //var jwtIssuer = builder.Configuration["Jwt:Issuer"];  // this is in local host 
        var jwtIssuer = "https://192.168.100.39:7236";

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidIssuer = jwtIssuer,
                   ValidAudience = jwtIssuer
               };
           });
        var allowedOrigins = new[] {
    "http://localhost:8080",        // Vite default
    "http://localhost:3000",        // React default
    "http://192.168.1.67:8000",     // Your LAN frontend
    "http://192.168.100.39:8000"    // Optional LAN frontend
};

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyCorsPolicy", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials(); // allow cookies & tokens
            });
        });

        builder.Services.AddAuthorization();

        // 🔹 Register Application Services
        builder.Services.AddScoped<ITeamRepository, TeamRepository>();
        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<IOurServicesRepository, OurServicesRepository>();
        builder.Services.AddScoped<IOurServicesService, OurServicesService>();
        builder.Services.AddScoped<IAppointmentRepository, AppointmentRepositiory>();
        builder.Services.AddScoped<IAppointmentService, AppointmentService>();
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();

        builder.Services.AddScoped<IAuthService, AuthServices>();

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddEndpointsApiExplorer();  // This is necessary for Swagger UI to display endpoints
        builder.Services.AddSwaggerGen();  // This generates Swagger docs for your API


        var app = builder.Build();
        app.UseCors("MyCorsPolicy");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}