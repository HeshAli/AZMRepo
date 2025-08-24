using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AZMAdmin.CMS.HomeBanners.Dto;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.HomeBanners
{
    public interface IHomeBannerAppService : IApplicationService
    {
        Task<HomeBannerDto> GetHomeBannerAsync(EntityDto<int> input);
        Task<PagedResultDto<HomeBannerDto>> GetHomeBannerListAsync(PagedHomeBannerResultRequestDto input);
        Task<HomeBannerDto> CreateHomeBannerAsync(CreateHomeBannerDto input);
        Task<HomeBannerDto> UpdateHomeBannerAsync(UpdateHomeBannerDto input);
        Task DeleteHomeBannerAsync(EntityDto<int> input);
        Task ActivateDeactivateHomeBanner(int id);
    }
}