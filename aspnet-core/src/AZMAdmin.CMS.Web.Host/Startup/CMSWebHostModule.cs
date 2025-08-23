using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AZMAdmin.CMS.Configuration;

namespace AZMAdmin.CMS.Web.Host.Startup
{
    [DependsOn(
       typeof(CMSWebCoreModule))]
    public class CMSWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CMSWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CMSWebHostModule).GetAssembly());
        }
    }
}
