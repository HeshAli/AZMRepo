using Abp.Zero.EntityFrameworkCore;
using AZMAdmin.CMS.Attachments;
using AZMAdmin.CMS.Authorization.Roles;
using AZMAdmin.CMS.Authorization.Users;
using AZMAdmin.CMS.ContentCategories;
using AZMAdmin.CMS.Contents;
using AZMAdmin.CMS.Courses;
using AZMAdmin.CMS.CoursesDetails;
using AZMAdmin.CMS.Footers;
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
        public DbSet<ContentCategory> ContentCategories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseDetails> CoursesDetails { get; set; }
    }
}