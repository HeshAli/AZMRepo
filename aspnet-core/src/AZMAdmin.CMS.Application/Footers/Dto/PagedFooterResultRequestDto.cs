using Abp.Application.Services.Dto;
using AZMAdmin.CMS.Enums;

namespace AZMAdmin.CMS.Footers.Dto
{
    public class PagedFooterResultRequestDto : PagedResultRequestDto
    {
        public FooterEnum? Type { get; set; }
        public bool? IsActive { get; set; }
        public string Filter { get; set; } // matches Code/NameAr/NameEn
    }
}