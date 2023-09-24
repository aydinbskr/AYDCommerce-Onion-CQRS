﻿using AYDCommerce.API.Application.Interfaces.Repositories;
using AYDCommerce.API.Persistence.Context;
using AYDCommerce.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Persistence
{
    public static class Registration
    {
        public static void AddPersistence(this IServiceCollection services,IConfiguration configuration) 
        {
            services
                .AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        }
    }
}
