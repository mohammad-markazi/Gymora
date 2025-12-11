using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gymora.Database
{
    public static class ConfigureService
    {
        public static IServiceCollection AddDatabaseConfigService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<GymoraDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IGymoraDbContext>(provider => provider.GetRequiredService<GymoraDbContext>());
            return services;
        }

    }
}
