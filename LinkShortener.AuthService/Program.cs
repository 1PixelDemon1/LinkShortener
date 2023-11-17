using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShortWeb.AuthService.Data;
using ShortWeb.AuthService.Models;
using ShortWeb.AuthService.Service;
using ShortWeb.AuthService.Service.IService;

namespace ShortWeb.AuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultPostgresConnection")));

            // Populating JwtOptions class with
            // information from appsettings.json.
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("ApiSettings:JwtOptions"));

            // Especially for Identity microservice.
            // Registring ApplicationUser as our Identity User model
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // This api is managing authentication, so we add this.
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            
            ApplyMigration(app);
            app.Run();
        }

        public static void ApplyMigration(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
        }
    }
}