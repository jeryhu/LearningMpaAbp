using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using LearningMpaAbp.Authorization;
using LearningMpaAbp.Authorization.Roles;
using LearningMpaAbp.MultiTenancy;
using LearningMpaAbp.Users;

namespace LearningMpaAbp
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class LearningMpaAbpCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);
            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = true;
            //Add/remove localization sources here

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    LearningMpaAbpConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), "LearningMpaAbp.Locatlization.Source")
                )
            );
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<LearningMpaAbpAuthorizationProvider>();
            //由于ABP是模块化开发，当需要为自己自定义的模块定义权限的时候需要注册自己实现的授权器AuthorizationProvider操作如下：
            Configuration.Authorization.Providers.Add<TaskAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}