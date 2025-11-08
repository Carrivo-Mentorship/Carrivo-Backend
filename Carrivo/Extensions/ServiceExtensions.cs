using Carrivo.Application.Interfaces;
using Carrivo.Application.Services.Auth;
using Carrivo.Application.Services.Email;
using Carrivo.Core.Interfaces;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CarrivoDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
