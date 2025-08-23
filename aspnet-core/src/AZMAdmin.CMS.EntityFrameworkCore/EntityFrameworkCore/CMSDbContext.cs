using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AZMAdmin.CMS.Authorization.Roles;
using AZMAdmin.CMS.Authorization.Users;
using AZMAdmin.CMS.MultiTenancy;

namespace AZMAdmin.CMS.EntityFrameworkCore
{
    public class CMSDbContext : AbpZeroDbContext<Tenant, Role, User, CMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public CMSDbContext(DbContextOptions<CMSDbContext> options)
            : base(options)
        {
        }
    }
}
