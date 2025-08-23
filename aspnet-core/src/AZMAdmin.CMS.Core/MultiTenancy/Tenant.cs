using Abp.MultiTenancy;
using AZMAdmin.CMS.Authorization.Users;

namespace AZMAdmin.CMS.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
