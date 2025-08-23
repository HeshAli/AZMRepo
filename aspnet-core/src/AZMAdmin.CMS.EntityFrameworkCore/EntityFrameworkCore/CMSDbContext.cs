using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AZMAdmin.CMS.Authorization.Roles;
using AZMAdmin.CMS.Authorization.Users;
using AZMAdmin.CMS.MultiTenancy;
using AZMAdmin.CMS.HomeBanners;

namespace AZMAdmin.CMS.EntityFrameworkCore
{
    public class CMSDbContext : AbpZeroDbContext<Tenant, Role, User, CMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public CMSDbContext(DbContextOptions<CMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<HomeBanner> HomeBanners { get; set; }
    }
}
