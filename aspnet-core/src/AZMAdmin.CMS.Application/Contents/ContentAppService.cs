using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.UI;
using AZMAdmin.CMS.ContentCategories;
using AZMAdmin.CMS.Contents.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Contents
{
    public class ContentAppService : CMSAppServiceBase, IContentAppService
    {
        private readonly IRepository<Content, int> _repo; 
        private readonly IRepository<ContentCategory, int> _Catrepo; 
        public ContentAppService(IRepository<Content, int> repository, IRepository<ContentCategory, int> Catrepo)
        {
            _repo = repository; 
            _Catrepo = Catrepo;
        }
        [AllowAnonymous]
        public async Task ActivateDeactivateContent(int id)
        {
            try
            {
                var content = await _repo.FirstOrDefaultAsync(id);

                if (content is null)
                    throw new UserFriendlyException(L("ContentNotExist"));


                content.IsActive = !(content.IsActive ?? false);

                await _repo.UpdateAsync(content);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {

                throw;
            }
           
        }

        public async Task<ContentDto> GetContentAsync(EntityDto<int> input)
        {
            var entity = await _repo.GetAsync(input.Id);
            if (entity is null)
                throw new UserFriendlyException(L("ContentNotExist", CultureInfo.CurrentCulture));
            return ObjectMapper.Map<ContentDto>(entity);
        }

        public async Task<PagedResultDto<ContentDto>> GetContentListAsync(PagedContentResultRequestDto input)
        {
            // Example filters (adjust to your DTO)
          
            var query = _repo.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NameAr.Contains(input.Filter) || x.NameEn.Contains(input.Filter))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                .WhereIf(input.ContentCategoryId.HasValue, x => x.ContentCategoryId == input.ContentCategoryId).AsQueryable();
            if (!string.IsNullOrEmpty(input.CategoryCode))
            {
                var filterCat =await _Catrepo.FirstOrDefaultAsync(s => s.Code == input.CategoryCode);
                if (filterCat !=null)
                {
                    query = query.Where(s => s.ContentCategoryId == filterCat.Id);
                }
            }
            var totalCount = await query.CountAsync();
            query = query.OrderByDescending(x => x.CreationTime);
                     
            var items = await query.ToListAsync();

            var dtos = ObjectMapper.Map<List<ContentDto>>(items);
            return new PagedResultDto<ContentDto>(totalCount, dtos);
        }

        [Authorize] // require login (remove if public)
        public async Task<ContentDto> CreateContentAsync(CreateContentDto input)
        {

            var currentCategory = await _Catrepo.FirstOrDefaultAsync(s => s.Code == input.CategoryCode);
            var entity = ObjectMapper.Map<Content>(input);
            entity.ContentCategoryId = currentCategory.Id;
            entity.IsActive = entity.IsActive ?? true;
            entity.AttachmentId = input.AttachmentId;

            var id = await _repo.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            var created = await _repo.GetAsync(id);
            return ObjectMapper.Map<ContentDto>(created);
        }

        [Authorize]
        public async Task<ContentDto> UpdateContentAsync(UpdateContentDto input)
        {
            var entity = await _repo.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("ContentNotExist"));

            // map incoming fields
            ObjectMapper.Map(input, entity);

            await _repo.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ContentDto>(entity);
        }

        [Authorize]
        public async Task DeleteContentAsync(EntityDto<int> input)
        {
            var entity = await _repo.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("ContentNotExist"));

            await _repo.DeleteAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }



    }
}