using AYDCommerce.API.Application.Interfaces.AutoMapper;
using AYDCommerce.API.Application.Interfaces.Tokens;
using AYDCommerce.API.Infrastructure.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();
        }
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper.Mapper>();
        }
    }
}
