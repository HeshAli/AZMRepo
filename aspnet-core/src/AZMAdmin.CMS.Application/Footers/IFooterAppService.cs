using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AZMAdmin.CMS.Footers.Dto;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Footers
{
    public interface IFooterAppService : IApplicationService
    {
        Task ActivateDeactivateFooter(int id);
        Task<FooterDto> CreateFooterAsync(CreateFooterDto input);
        Task<FooterDto> UpdateFooterAsync(UpdateFooterDto input);
        Task DeleteFooterAsync(EntityDto<int> input);
        Task<FooterDto> GetFooterAsync(EntityDto<int> input);
        Task<PagedResultDto<FooterDto>> GetFooterListAsync(PagedFooterResultRequestDto input);
    }
}