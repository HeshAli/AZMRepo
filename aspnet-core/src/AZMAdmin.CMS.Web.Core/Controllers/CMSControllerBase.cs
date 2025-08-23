using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AZMAdmin.CMS.Controllers
{
    public abstract class CMSControllerBase: AbpController
    {
        protected CMSControllerBase()
        {
            LocalizationSourceName = CMSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
