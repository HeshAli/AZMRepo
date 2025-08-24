using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AZMAdmin.CMS.ContentCategories.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.ContentCategories
{
    public class ContentCategoryAppService : CMSAppServiceBase, IContentCategoryAppService
    {
        private readonly IRepository<ContentCategory, int> _repository;
        public ContentCategoryAppService(IRepository<ContentCategory, int> repository) 
        {
            _repository = repository;
        }
        public async Task ActivateDeactivateContentCategory(int id)
        {
            var contentCategory = await _repository.FirstOrDefaultAsync(id);

            if (contentCategory is null)
                throw new UserFriendlyException(L("ContentCategoryNotExist",CultureInfo.CurrentCulture));


            contentCategory.IsActive = !(contentCategory.IsActive ?? false);

            await _repository.UpdateAsync(contentCategory);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<ContentCategoryDto> GetContentCategoryAsync(EntityDto<int> input)
        {
            var entity = await _repository.GetAsync(input.Id);
            return ObjectMapper.Map<ContentCategoryDto>(entity);
        }

        public async Task<PagedResultDto<ContentCategoryDto>> GetContentCategoryListAsync(PagedContentCategoryResultRequestDto input)
        {
            var query = _repository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NameAr.Contains(input.Filter) || x.NameEn.Contains(input.Filter))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive).AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreationTime)
                .PageBy(input)    // requires PagedAndSortedResultRequestDto
                .ToListAsync();

            var dtos = ObjectMapper.Map<List<ContentCategoryDto>>(items);
            return new PagedResultDto<ContentCategoryDto>(totalCount, dtos);
        }

        public async Task<ContentCategoryDto> CreateContentCategoryAsync(CreateContentCategoryDto input)
        {

            var entity = ObjectMapper.Map<ContentCategory>(input);
            entity.IsActive ??= true;

            var id = await _repository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            var created = await _repository.GetAsync(id);
            return ObjectMapper.Map<ContentCategoryDto>(created);
        }

        public async Task<ContentCategoryDto> UpdateContentCategoryAsync(UpdateContentCategoryDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("ContentCategoryNotExist"));

            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ContentCategoryDto>(entity);
        }

        public async Task DeleteContentCategoryAsync(EntityDto<int> input)
        {
            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("ContentCategoryNotExist"));

            await _repository.DeleteAsync(entity); // respects soft-delete from FullAuditedEntity
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}