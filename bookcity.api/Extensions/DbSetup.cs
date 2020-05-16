﻿using bookcity.entitys.models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace bookcity.Extensions
{
    /// <summary>
    /// Db 启动服务
    /// </summary>
    public static class DbSetup
    {
        public static void AddDbSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.AddScoped<DBSeed>();
            //services.AddScoped<MyContext>();
        }
    }
}
