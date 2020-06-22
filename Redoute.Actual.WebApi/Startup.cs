using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Redoute.Actualsis.Services.Master;

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(Actualsis.IRepositonry.IBasicRepository<>), typeof(Actualsis.Repositonry.BasicRepository<>));
            services.AddSingleton<ISysUserService, SysUserService>();

            #region 依赖注入

            //var builder = new ContainerBuilder();//实例化容器
            ////注册所有模块module
            //builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            ////获取所有的程序集
            ////var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            //var assemblys = RuntimeHelper.GetAllAssemblies().ToArray();

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

            //返回json  大小写
            services.AddMvc().AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
