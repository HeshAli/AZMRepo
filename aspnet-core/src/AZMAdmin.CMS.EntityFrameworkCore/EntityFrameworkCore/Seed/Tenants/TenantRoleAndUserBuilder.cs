using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.MultiTenancy;
using AZMAdmin.CMS.Authorization;
using AZMAdmin.CMS.Authorization.Roles;
using AZMAdmin.CMS.Authorization.Users;
using AZMAdmin.CMS.ContentCategories;
using AZMAdmin.CMS.Contents;
using AZMAdmin.CMS.Enums;
using AZMAdmin.CMS.Footers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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

            var categoryGallery = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Home_Gallery");
            if (categoryGallery == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Home_Gallery",
                    IsActive = true,
                    NameAr = "معرض الصور للصفحة الرئيسية",
                    NameEn = "Home Gallery"

                };

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges();
            }

            var categoryGeneralObjectives = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Course_GeneralObjectives");
            if (categoryGeneralObjectives == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Course_GeneralObjectives",
                    IsActive = true,
                    NameAr = "الأهداف العامة",
                    NameEn = "General Objectives"

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

            var categoryCourseContent = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Course_Content");
            if (categoryCourseContent == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Course_Content",
                    IsActive = true,
                    NameAr = "البرامج التدريبية",
                    NameEn = "Training Courses"

                };

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges();
                categoryCourseContent = CatAzm;
            }

            var Contentcourse = _context.Contents.IgnoreQueryFilters().FirstOrDefault(u => u.ContentCategoryId == categoryCourseContent.Id);
            if (Contentcourse == null)
            {
                var content = new Content()
                {
                    ContentCategoryId = categoryCourseContent.Id,
                    IsActive = true,
                    NameAr = "برنامج السّلامة والصحّة المهنيّة هو أداة مبتكرة تواكب تسارعًا اقتصاديًا في المملكة وما يتطلّبه سوق العمل، ويسعى إلى خلق بيئة استثماريّة جاذبة ومستدامة تُعزّز من تنافسيّة الاقتصاد الوطني. ويركّز البرنامج بشكلٍ رئيسي على تدريب الباحثين عن عمل على المهارات والمعارف والسلوكيات المهنيّة المرتبطة بمجال السّلامة والصحّة المهنيّة بشكلٍ تفاعلي، وفق أفضل الممارسات الدوليّة في مجال التدريب التقني والمهني.\r\nويُعدّ برنامج السّلامة والصحّة المهنيّة إحدى المبادرات الوطنيّة الهادفة إلى تعزيز بيئة العمل في المملكة العربيّة السعوديّة من خلال إعداد كفاءات متخصّصة في هذا المجال. كما يركّز البرنامج على تمكين الممارسين والمحترفين من اكتساب المعرفة والمهارات والسلوكيات المثلى التي تؤهّلهم لإدارة المخاطر المهنيّة بكفاءة عالية.",
                    NameEn = "The Occupational Safety and Health Program is an innovative tool that keeps pace with the Kingdom’s rapid economic growth and the evolving needs of the labor market. It seeks to create an attractive and sustainable investment environment that strengthens the competitiveness of the national economy. The program primarily focuses on training job seekers in the skills, knowledge, and professional behaviors related to occupational safety and health, delivered interactively and aligned with international best practices in technical and vocational training.\r\nThe program is one of the national initiatives aimed at enhancing the work environment in the Kingdom of Saudi Arabia by preparing specialized competencies in occupational safety and health. It also focuses on enabling practitioners and professionals to acquire the knowledge, skills, and optimal behaviors that qualify them to manage occupational risks with high efficiency."

                };

                _context.Contents.Add(content);
                _context.SaveChanges();
            }


            var categoryAboutUs = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "AboutUs");
            if (categoryAboutUs == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "AboutUs",
                    IsActive = true,
                    NameAr = "عن عزم",
                    NameEn = "About Azm"

                };

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges();
                categoryAboutUs = CatAzm;
            }

            var ContentaboutUs = _context.Contents.IgnoreQueryFilters().FirstOrDefault(u => u.ContentCategoryId == categoryAboutUs.Id);
            if (ContentaboutUs == null)
            {
                var content = new Content()
                {
                    ContentCategoryId = categoryAboutUs.Id,
                    IsActive = true,
                    NameAr = "منصّة تعليمية متكاملة تهدف إلى تمكين الأفراد وتنمية قدراتهم من خلال تقديم مجموعة متخصّصة ومتميّزة من البرامج التدريبية، بما في ذلك «هندسة المواقع الإلكترونية» و«إدارة الفعاليات». تتميّز المنصّة بما يلي:",
                    NameEn = "A comprehensive educational platform aiming to empower individuals and develop their skills by offering a specialized and distinguished set of training programs, including \"Website Engineering\" and \"Event Management.\" The platform is distinguished by the following:"

                };

                _context.Contents.Add(content);
                _context.SaveChanges();
            }

            var categoryQuestions = _context.ContentCategories.IgnoreQueryFilters().FirstOrDefault(u => u.Code == "Questions");
            if (categoryQuestions == null)
            {
                var CatAzm = new ContentCategory()
                {
                    Code = "Questions",
                    IsActive = true,
                    NameAr = "الأسئلة الشائعة",
                    NameEn = "General questions"

                };

                _context.ContentCategories.Add(CatAzm);
                _context.SaveChanges(); 
            }

            var languages = _context.Languages.IgnoreQueryFilters().Where(u => u.Name != "en" && u.Name != "ar").ToList();
            if (languages.Any())
            {
                _context.Languages.RemoveRange(languages);
                _context.SaveChanges();
            }
            PrepareFooter();

        }

        private void PrepareFooter()
        {
            var footerItems = new List<Footer>
{
    // Right Column (About / Links)
    new Footer
    {
        Code = "AboutUs",
        NameAr = "من نحن",
        NameEn = "About Us",
        RedirectUrl = "/about",
        IsActive = true,
        Type = FooterEnum.Links
    },
    new Footer
    {
        Code = "Academy",
        NameAr = "الأكاديمية",
        NameEn = "Academy",
        RedirectUrl = "/academy",
        IsActive = true,
        Type = FooterEnum.Links
    },
    new Footer
    {
        Code = "Foundation",
        NameAr = "التأسيس والتشغيل",
        NameEn = "Foundation & Operation",
        RedirectUrl = "/foundation",
        IsActive = true,
        Type = FooterEnum.Links
    },
    new Footer
    {
        Code = "TrainingPrograms",
        NameAr = "البرامج التدريبية",
        NameEn = "Training Programs",
        RedirectUrl = "/programs",
        IsActive = true,
        Type = FooterEnum.Links
    },
    new Footer
    {
        Code = "Careers",
        NameAr = "التوظيف",
        NameEn = "Careers",
        RedirectUrl = "/careers",
        IsActive = true,
        Type = FooterEnum.Links
    },

    // Middle Column (Media Center)
    new Footer
    {
        Code = "MediaCenter",
        NameAr = "المركز الإعلامي",
        NameEn = "Media Center",
        RedirectUrl = "/media-center",
        IsActive = true,
        Type = FooterEnum.Links
    },
    new Footer
    {
        Code = "PressReleases",
        NameAr = "البيانات الإعلامية",
        NameEn = "Press Releases",
        RedirectUrl = "/press-releases",
        IsActive = true,
        Type = FooterEnum.Links
    },
    new Footer
    {
        Code = "NewsReports",
        NameAr = "التقارير الإخبارية",
        NameEn = "News Reports",
        RedirectUrl = "/news-reports",
        IsActive = true,
        Type = FooterEnum.Links
    },
    new Footer
    {
        Code = "FAQ",
        NameAr = "أسئلة وأجوبة",
        NameEn = "FAQ",
        RedirectUrl = "/faq",
        IsActive = true,
        Type = FooterEnum.Links
    },
    new Footer
    {
        Code = "Certificates",
        NameAr = "الشهادات",
        NameEn = "Certificates",
        RedirectUrl = "/certificates",
        IsActive = true,
        Type = FooterEnum.Links
    },

    // Left Column (Social Media)
    new Footer
    {
        Code = "Facebook",
        NameAr = "فيسبوك",
        NameEn = "Facebook",
        RedirectUrl = "https://www.facebook.com",
        IsActive = true,
        Type = FooterEnum.SocialMedia
    },
    new Footer
    {
        Code = "YouTube",
        NameAr = "يوتيوب",
        NameEn = "YouTube",
        RedirectUrl = "https://www.youtube.com",
        IsActive = true,
        Type = FooterEnum.SocialMedia
    },
    new Footer
    {
        Code = "Twitter",
        NameAr = "تويتر",
        NameEn = "Twitter",
        RedirectUrl = "https://www.twitter.com",
        IsActive = true,
        Type = FooterEnum.SocialMedia
    },
    new Footer
    {
        Code = "Instagram",
        NameAr = "إنستغرام",
        NameEn = "Instagram",
        RedirectUrl = "https://www.instagram.com",
        IsActive = true,
        Type = FooterEnum.SocialMedia
    },
    new Footer
    {
        Code = "LinkedIn",
        NameAr = "لينكدإن",
        NameEn = "LinkedIn",
        RedirectUrl = "https://www.linkedin.com",
        IsActive = true,
        Type = FooterEnum.SocialMedia
    }
};

            // Insert if not exists
            foreach (var footer in footerItems)
            {
                var exists = _context.Footers.IgnoreQueryFilters().FirstOrDefault(f => f.Code == footer.Code);
                if (exists == null)
                {
                    _context.Footers.Add(footer);
                    _context.SaveChanges();
                }
            }
        }
    }
}
