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
            var category = await _repository.FirstOrDefaultAsync(s=>s.Code ==code && s.IsActive==true);
            if (category is null)
            {
                return new ContentCategoryDto();
            }
               return ObjectMapper.Map<ContentCategoryDto>(category); ;
        }

        public async Task<List<ContentWithAttachmentDto>> GetByCategoryCode(string code)
        {
            // Include relations so we can filter by category code and read attachment id
            var query = ( _contentRepo.GetAllIncluding(x => x.ContentCategory, x => x.Attachment))
                .Where(x => x.ContentCategory.Code == code && x.IsActive ==true);

            var entities = await query.ToListAsync();

            // Project → DTOs and build AttachmentUrl if AttachmentId exists
            var result = entities.Select(x => new ContentWithAttachmentDto
            {
                Id = x.Id,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                DescriptionAr = x.DescriptionAr,
                DescriptionEn = x.DescriptionEn,
                RedirectUrl = x.RedirectUrl,
                IsActive = x.IsActive,

                CategoryCode = x.ContentCategory?.Code,
                AttachmentId = x.AttachmentId, 
                AttachmentUrl = x.AttachmentId.HasValue ? x.Attachment?.Path : null
            }).ToList();

            return result;
        }


        public async Task<List<HomeBannerDto>> getHomeBanners()
        {
            var homeBanners = await _homeBannserRepo.GetAllListAsync(s => s.IsActive == true);
            return ObjectMapper.Map<List<HomeBannerDto>>(homeBanners);
        }
    }
}
