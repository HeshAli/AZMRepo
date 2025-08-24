using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.HomeBanners.Dto
{
    public class PagedHomeBannerResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }     // searches NameAr/NameEn
        public bool? IsActive { get; set; }
    }
}