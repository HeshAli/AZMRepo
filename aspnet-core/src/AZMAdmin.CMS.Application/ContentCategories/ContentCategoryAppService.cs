using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AZMAdmin.CMS.ContentCategories.Dto;
using System.Globalization;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.ContentCategories
{
    public class ContentCategoryAppService : AsyncCrudAppService<ContentCategory, ContentCategoryDto, int, PagedContentCategoryResultRequestDto, CreateContentCategoryDto, UpdateContentCategoryDto, GetContentCategoryDto, DeleteContentCategoryDto>, IContentCategoryAppService
    {
        private readonly IRepository<ContentCategory, int> _repository;
        public ContentCategoryAppService(IRepository<ContentCategory, int> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task ActivateDeactivateContentCategory(int id)
        {
            var contentCategory = await _repository.FirstOrDefaultAsync(id);

            if (contentCategory is null)
                throw new UserFriendlyException(L("ContentCategoryNotExist", new CultureInfo("ar")));


            contentCategory.IsActive = !(contentCategory.IsActive ?? false);

            await _repository.UpdateAsync(contentCategory);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}