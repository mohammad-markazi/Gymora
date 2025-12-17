using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database;
using Gymora.Service.Movement;
using Gymora.Service.User;
using Gymora.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gymora.Service
{
    public static class ConfigureService
    {
        public static IServiceCollection AddServiceConfigService(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddScoped<IVerifyCodeService, VerifyCodeService>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<ILogService,LogService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovementService, MovementService>();


            return services;
        }
    }
}
