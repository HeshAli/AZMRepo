using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using AZMAdmin.CMS.Authorization.Roles;
using AZMAdmin.CMS.Authorization.Users;
using AZMAdmin.CMS.Configuration;
using AZMAdmin.CMS.Localization;
using AZMAdmin.CMS.MultiTenancy;
using AZMAdmin.CMS.Timing;

namespace AZMAdmin.CMS
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class CMSCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            CMSLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = CMSConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("ar", "عربي", "famfamfam-flags sa"));
            
            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = CMSConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = CMSConsts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CMSCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
