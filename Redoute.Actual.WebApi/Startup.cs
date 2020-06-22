using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Redoute.Actualsis.IServices;
using Redoute.Actualsis.Model;

namespace Redoute.Actual.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //返回json  大小写
            services.AddMvc().AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // 注入仓储
            //services.AddSingleton(typeof(Actualsis.IRepositonry.IBasicRepository<>), typeof(Actualsis.Repositonry.BasicRepository<>));
            // services.Register
            // 注入服务
            //services.AddSingleton<ISysUserService, SysUserService>();

            #region 依赖注入

            //var builder = new ContainerBuilder();//实例化容器

            ////注册所有模块module
            //builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            ////获取所有的程序集
            ////var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            //var assemblys = RuntimeHelpers.GetAllAssemblies().ToArray();

            ////注册所有继承IDependency接口的类
            //builder.RegisterAssemblyTypes().Where(type => typeof(IDependency).IsAssignableFrom(type) && !type.IsAbstract);
            ////注册仓储，所有IRepository接口到Repository的映射
            //builder.RegisterAssemblyTypes(assemblys).Where(t => t.Name.EndsWith("Repository") && !t.Name.StartsWith("I")).AsImplementedInterfaces();
            ////注册服务，所有IApplicationService到ApplicationService的映射
            ////builder.RegisterAssemblyTypes(assemblys).Where(t => t.Name.EndsWith("AppService") && !t.Name.StartsWith("I")).AsImplementedInterfaces();
            //builder.Populate(services);
            //ApplicationContainer = builder.Build();

            //return new AutofacServiceProvider(ApplicationContainer); //第三方IOC接管 core内置DI容器 
            ////return services.BuilderInterceptableServiceProvider(builder => builder.SetDynamicProxyFactory());
            #endregion

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            #region AutoFac
            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();

            // ****如果你是第一次下载项目，请先F6编译，然后再F5执行，※※★※※
            // **** 因为解耦，bin文件夹没有以下两个dll文件，会报错，所以先编译生成这两个dll ※※★※※


            //获取项目绝对路径，请注意，这个是实现类的dll文件，不是接口 IService.dll ，注入容器当然是Activatore
            var servicesDllFile = Path.Combine(basePath, "Redoute.Actualsis.Services.dll");
            var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法

            //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。

            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors();//引用Autofac.Extras.DynamicProxy;
                                                     // 如果你想注入两个，就这么写  InterceptedBy(typeof(BlogCacheAOP), typeof(BlogLogAOP));
                                                     //.InterceptedBy(typeof(BlogCacheAOP));//允许将拦截器服务的列表分配给注册。

            var repositoryDllFile = Path.Combine(basePath, "Redoute.Actualsis.Repositonry.dll");
            var assemblysRepository = Assembly.LoadFile(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            #endregion

   

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
