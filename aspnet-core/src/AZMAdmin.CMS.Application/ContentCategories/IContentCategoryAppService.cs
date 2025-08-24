using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AZMAdmin.CMS.ContentCategories.Dto;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.ContentCategories
{
    public interface IContentCategoryAppService : IApplicationService
    {
        Task<ContentCategoryDto> GetContentCategoryAsync(EntityDto<int> input);
        Task<PagedResultDto<ContentCategoryDto>> GetContentCategoryListAsync(PagedContentCategoryResultRequestDto input);
        Task<ContentCategoryDto> CreateContentCategoryAsync(CreateContentCategoryDto input);
        Task<ContentCategoryDto> UpdateContentCategoryAsync(UpdateContentCategoryDto input);
        Task DeleteContentCategoryAsync(EntityDto<int> input);
        Task ActivateDeactivateContentCategory(int id);
    }
}