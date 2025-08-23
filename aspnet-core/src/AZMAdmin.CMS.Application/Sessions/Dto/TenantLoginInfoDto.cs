using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AZMAdmin.CMS.MultiTenancy;

namespace AZMAdmin.CMS.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
