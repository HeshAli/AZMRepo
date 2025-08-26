using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using AZMAdmin.CMS.Authorization;
using AZMAdmin.CMS.Authorization.Roles;
using AZMAdmin.CMS.Authorization.Users;
using AZMAdmin.CMS.ContentCategories;
using AZMAdmin.CMS.Contents;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace AZMAdmin.CMS.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly CMSDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(CMSDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // Admin role

            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to admin role

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == adminRole.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(new CMSAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    })
                );
                _context.SaveChanges();
            }

            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }


            var categoryTrainingAzm = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Home_TrainingWithAzm");
            if (categoryTrainingAzm == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Home_TrainingWithAzm",
                    IsActive = true,
                    NameAr = "لماذا تتدرب مع عزم؟",
                    NameEn = "Training With Azm?"

                }; 

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges(); 
            }

            var categoryTargetAudience = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Home_TargetAudience");
            if (categoryTargetAudience == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Home_TargetAudience",
                    IsActive = true,
                    NameAr = "الفئة المستهدفة",
                    NameEn = "Target Audience"

                };

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges();
            }

            var categoryProgressSteps = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Home_ProgressContent");
            if (categoryProgressSteps == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Home_ProgressContent",
                    IsActive = true,
                    NameAr = "محتوى خطوات التقدم",
                    NameEn = "Progress Steps Content"

                };

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges();
                categoryProgressSteps = CatAzm;
            }

            var ContentProgressSteps = _context.Contents.IgnoreQueryFilters().FirstOrDefault(u => u.ContentCategoryId == categoryProgressSteps.Id);
            if (ContentProgressSteps == null)
            {
                var content = new Content()
                {
                    ContentCategoryId = categoryProgressSteps.Id,
                    IsActive = true,
                    NameAr = "ابدأ رحلة تطويرك المهني مع أكاديمية عزم واتخذ الخطوة الأولى نحو مسيرة مهنية مُثرية، واحصل على المهارات والموارد اللازمة لتحقيق النجاح.",
                    NameEn = "Start your professional development journey with Azm Academy and take the first step toward an enriching career path. Gain the skills and resources you need to achieve success."

                };

                _context.Contents.Add(content);
                _context.SaveChanges();
            }

            var categoryHome_ProgressSteps = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Home_ProgressSteps");
            if (categoryHome_ProgressSteps == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Home_ProgressSteps",
                    IsActive = true,
                    NameAr = "خطوات التقدم",
                    NameEn = "Progress Steps"

                };

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges(); 
            }

            var categoryHome_AcceptanceCriteria = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Home_AcceptanceCriteria");
            if (categoryHome_AcceptanceCriteria == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Home_AcceptanceCriteria",
                    IsActive = true,
                    NameAr = "معايير القبول",
                    NameEn = "Acceptance Criteria"

                };

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges(); 
            }
        }
    }
}
