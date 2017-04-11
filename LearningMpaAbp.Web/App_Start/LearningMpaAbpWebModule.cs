using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Runtime.Caching.Redis;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using LearningMpaAbp.Api;
using Hangfire;

namespace LearningMpaAbp.Web
{
    /*
        说明： Abp在启动项目时根据指定的启动模块（StartupModule）首先加载该模块，
        然后再去检查该模块的自定义特性是否定义有[DependsOn]特性，若有则按序加载所有依赖的模块，也就是链式动态依赖加载。
        然后再依次调用Module的PreInitialize，Initialize和PostInitialize以完成初始化。
     */
    [DependsOn(
        typeof(LearningMpaAbpDataModule),
        typeof(LearningMpaAbpApplicationModule),
        typeof(LearningMpaAbpWebApiModule),
        typeof(AbpWebSignalRModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule),
        typeof(AbpRedisCacheModule))]
    public class LearningMpaAbpWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<LearningMpaAbpNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});

            //配置使用Redis缓存
            //Configuration.Caching.UseRedis();

            //配置所有Cache的默认过期时间为2小时
            Configuration.Caching.ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(2);
            });

            //配置指定的Cache过期时间为10分钟
            Configuration.Caching.Configure("ControllerCache", cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(10);
            });

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
