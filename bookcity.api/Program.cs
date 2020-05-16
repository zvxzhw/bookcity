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

            // ���������ڽ��������������� Microsoft.Extensions.DependencyInjection.IServiceScope��
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                //var logger = loggerFactory.CreateLogger<Program>();
                //logger.LogError("asdasdasdads", args);

                //try
                //{
                //    // �� system.IServicec�ṩ�����ȡ T ���͵ķ���
                //    // ���ݿ������ַ������� Model ��� Seed �ļ����µ� MyContext.cs ��
                //    var configuration = services.GetRequiredService<IConfiguration>();
                //    if (configuration.GetSection("AppSettings")["SeedDBEnabled"].ObjToBool() || configuration.GetSection("AppSettings")["SeedDBDataEnabled"].ObjToBool())
                //    {
                //        // var myContext = services.GetRequiredService<MyContext>();
                //        // DBSeed.SeedAsync(myContext).Wait();
                //        var logger = loggerFactory.CreateLogger<Program>();
                //        logger.LogInformation("��ʱ���� system.IServicec�ṩ�����ȡ T ���͵ķ���");
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
                         serverOptions.AllowSynchronousIO = true;//����ͬ�� IO
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
