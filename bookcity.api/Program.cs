using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace bookcity.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // 创建可用于解析作用域服务的新 Microsoft.Extensions.DependencyInjection.IServiceScope。
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                //var logger = loggerFactory.CreateLogger<Program>();
                //logger.LogError("asdasdasdads", args);

                //try
                //{
                //    // 从 system.IServicec提供程序获取 T 类型的服务。
                //    // 数据库连接字符串是在 Model 层的 Seed 文件夹下的 MyContext.cs 中
                //    var configuration = services.GetRequiredService<IConfiguration>();
                //    if (configuration.GetSection("AppSettings")["SeedDBEnabled"].ObjToBool() || configuration.GetSection("AppSettings")["SeedDBDataEnabled"].ObjToBool())
                //    {
                //        // var myContext = services.GetRequiredService<MyContext>();
                //        // DBSeed.SeedAsync(myContext).Wait();
                //        var logger = loggerFactory.CreateLogger<Program>();
                //        logger.LogInformation("暂时不从 system.IServicec提供程序获取 T 类型的服务。");
                //    }
                //}
                //catch (Exception e)
                //{
                //    var logger = loggerFactory.CreateLogger<Program>();
                //    logger.LogError(e, "Error occured seeding the Database.");
                //    throw;
                //}
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //<--NOTE THIS
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                     {
                         serverOptions.AllowSynchronousIO = true;//启用同步 IO
                     })
                    .UseKestrel(options =>
                    {
                        options.Limits.MaxRequestBufferSize = 302768;
                        options.Limits.MaxRequestLineSize = 302768;
                    })
                     // .UseIISIntegration()
                     .UseStartup<Startup>()
                     .UseUrls(new string[] { "http://*:5000", "http://*:5001" }); 
 
                });
    }
}
