using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using AZMAdmin.CMS.ContentCategories;
using AZMAdmin.CMS.ContentCategories.Dto;
using AZMAdmin.CMS.Contents;
using AZMAdmin.CMS.Contents.Dto;
using AZMAdmin.CMS.Courses;
using AZMAdmin.CMS.HomeBanners;
using AZMAdmin.CMS.HomeBanners.Dto;
using AZMAdmin.CMS.Portal.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Portal
{
    public class PortalManagementService : CMSAppServiceBase, IPortalManagementService
    {
        private readonly IRepository<ContentCategory, int> _repository;
        private readonly IRepository<Content, int> _contentRepo;
        private readonly IRepository<HomeBanner, int> _homeBannserRepo;
        public PortalManagementService(IRepository<ContentCategory, int> repository,
            IRepository<Content, int> contentRepo, IRepository<HomeBanner, int> homeBannserRepo)
        {
            _repository = repository;
            _contentRepo = contentRepo;
            _homeBannserRepo = homeBannserRepo;
        }

        public async Task<ContentCategoryDto> GetByCode(string code)
        {
            var category = await _repository.FirstOrDefaultAsync(s => s.Code == code && s.IsActive == true);
            if (category is null)
            {
                return new ContentCategoryDto();
            }
            return ObjectMapper.Map<ContentCategoryDto>(category); ;
        }

        public async Task<List<ContentWithAttachmentDto>> GetByCategoryCode(string code)
        {
            // Include relations so we can filter by category code and read attachment id
            var query = (_contentRepo.GetAllIncluding(x => x.ContentCategory, x => x.Attachment))
                .Where(x => x.ContentCategory.Code == code && x.IsActive == true);

            var entities = await query.ToListAsync();

            // Project → DTOs and build AttachmentUrl if AttachmentId exists
            var result = entities.Select(x => new ContentWithAttachmentDto
            {
                Id = x.Id,
                DisplayName =CultureInfo.CurrentCulture.Name=="en"? x.NameEn:x.NameAr,
                DisplayDescription = CultureInfo.CurrentCulture.Name == "en" ? x.DescriptionEn : x.DescriptionAr,
                RedirectUrl = x.RedirectUrl,
                IsActive = x.IsActive,

                CategoryCode = x.ContentCategory?.Code,
                AttachmentId = x.AttachmentId,
                AttachmentUrl = x.AttachmentId.HasValue ? x.Attachment?.Path : null
            }).ToList();

            return result;
        }


        public async Task<List<PortalHomeBanner>> GetHomeBanners()
        {
            var homeBanners =  _homeBannserRepo.GetAllIncluding(s => s.Image, s => s.Logo)
                .Where(s=>s.IsActive ==true).Select(mod => new PortalHomeBanner
                {
                Id = mod.Id,
                DisplayName = CultureInfo.CurrentCulture.Name == "en" ? mod.NameEn : mod.NameAr,
                ImageURL = mod.ImageId !=null? mod.Image.Path:null,
                LogoURL = mod.LogoId != null ? mod.Logo.Path : null,
                }).ToList();
            return homeBanners;
        }
    }
}
