using Abp.Application.Services;
using AZMAdmin.CMS.MultiTenancy.Dto;

namespace AZMAdmin.CMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

