using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using bookcity.common;
using bookcity.common.Helper;
using bookcity.Extensions;
using bookcity.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using static bookcity.SwaggerHelper.CustomApiVersion;

namespace bookcity.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new Appsettings(Env.ContentRootPath));

            services.AddControllers();
            services.AddSqlsugarSetup();
            services.AddAutoMapperSetup();
            services.AddCorsSetup();
            services.AddSwaggerSetup();

            services.AddHttpContextSetup();
            services.AddAuthorizationSetup();

            services.AddControllers(o =>
            {
                // ȫ���쳣����
                o.Filters.Add(typeof(GlobalExceptionsFilter));
                // ȫ��·��Ȩ�޹�Լ
                o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                // ȫ��·��ǰ׺��ͳһ�޸�·��
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
            //ȫ������Json���л�����
            .AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //��ʹ���շ���ʽ��key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
                //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {

            String basePath = AppContext.BaseDirectory;
            // ��������� ������ǵ�һ��������Ŀ������F6���룬Ȼ����F5ִ�У����������

            #region ���нӿڲ�ķ���ע��

            #region Service.dll ע�룬�ж�Ӧ�ӿ�
            //��ȡ��Ŀ����·������ע�⣬�����ʵ�����dll�ļ������ǽӿ� IService.dll ��ע��������Ȼ��Activatore
            try
            {
                var servicesDllFile = Path.Combine(basePath, "bookcitys.services.dll");
                var assemblysServices = Assembly.LoadFrom(servicesDllFile);//ֱ�Ӳ��ü����ļ��ķ���  ��������� ������ǵ�һ��������Ŀ������F6���룬Ȼ����F5ִ�У����������

                //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();//ָ����ɨ������е�����ע��Ϊ�ṩ������ʵ�ֵĽӿڡ�

                var cacheType = new List<Type>();
                //if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
                //{
                //    cacheType.Add(typeof(RedisCacheAOP));
                //}
                //if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
                //{
                //    cacheType.Add(typeof(CacheAOP));
                //}
                //if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
                //{
                //    cacheType.Add(typeof(TranAOP));
                //}
                //if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
                //{
                //    cacheType.Add(typeof(LogAOP));
                //}

                builder.RegisterAssemblyTypes(assemblysServices)
                          .AsImplementedInterfaces()
                          .InstancePerLifetimeScope()
                          .EnableInterfaceInterceptors()//����Autofac.Extras.DynamicProxy;
                                                        // �������ע������������ôд  InterceptedBy(typeof(CacheAOP), typeof(LogAOP));
                                                        // �����ʹ��Redis���棬����뿪�� redis ���񣬶˿ں��ҵ���6379�������һ��������Ч��������ʹ��memory���� CacheAOP
                          .InterceptedBy(cacheType.ToArray());//����������������б�����ע�ᡣ 
                #endregion

                #region Repository.dll ע�룬�ж�Ӧ�ӿ�
                var repositoryDllFile = Path.Combine(basePath, "bookcity.repository.dll");
                var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
                builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();
            }
            catch (Exception ex)
            {
                //log.Error("Repository.dll��service.dll ��ʧ����Ϊ��Ŀ�����ˣ�������Ҫ��F6���룬��F5���У����鲢������\n" + ex.Message);

                //throw new Exception("��������� ������ǵ�һ��������Ŀ�����ȶ������������dotnet build��F6���룩��Ȼ���ٶ�api�� dotnet run��F5ִ�У���\n��Ϊ�����ˣ�������Ƿ�����ģʽ������bin�ļ����Ƿ����Repository.dll��service.dll ���������" + ex.Message + "\n" + ex.InnerException);
            }
            #endregion
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //���ݰ汾���Ƶ��� ����չʾ
                var ApiName = Appsettings.app(new string[] { "Startup", "ApiName" });
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                // ��swagger��ҳ�����ó������Զ����ҳ�棬�ǵ�����ַ�����д�������������.index.html
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("SerpApi.index.html");//���������MiniProfiler�������ܼ�صģ������£���������AOP�Ľӿ����ܷ�����������㲻��Ҫ��������ʱ��ע�͵�����Ӱ���֡�
                //c.RoutePrefix = "swagger"; //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
            });
            #endregion
            //ServiceLocator.Configure(app.ApplicationServices);

            app.UseCors("LimitRequests");

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);

            // ʹ�þ�̬�ļ�
            app.UseStaticFiles();

            // ��ת��ָ��ҳ�棬������Response.StatusCode
            app.UseStatusCodePagesWithReExecute("/api/Error/{0}");

            app.UseRouting();
            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();
                 endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            ConsoleHelper.WriteSuccessLine("##############################################");
            ConsoleHelper.WriteSuccessLine("#      Kestrel server has been started.      #");
            ConsoleHelper.WriteSuccessLine("##############################################");
            Console.WriteLine(" ");
        }
    }
}
