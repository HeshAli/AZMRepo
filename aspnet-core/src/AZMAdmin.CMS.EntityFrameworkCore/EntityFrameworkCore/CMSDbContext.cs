using Abp.Zero.EntityFrameworkCore;
using AZMAdmin.CMS.Attachments;
using AZMAdmin.CMS.Authorization.Roles;
using AZMAdmin.CMS.Authorization.Users;
using AZMAdmin.CMS.HomeBanners;
using AZMAdmin.CMS.MultiTenancy;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Attachment> Attachments { get; set; }
    }
}