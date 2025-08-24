using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AZMAdmin.CMS.Contents.Dto;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Contents
{
    public interface IContentAppService: IApplicationService
    {
        Task ActivateDeactivateContent(int id);
        Task<ContentDto> GetContentAsync(EntityDto<int> input);
        Task<PagedResultDto<ContentDto>> GetContentListAsync(PagedContentResultRequestDto input);
        Task<ContentDto> CreateContentAsync(CreateContentDto input);
        Task<ContentDto> UpdateContentAsync(UpdateContentDto input);
        Task DeleteContentAsync(EntityDto<int> input); 
    }
}