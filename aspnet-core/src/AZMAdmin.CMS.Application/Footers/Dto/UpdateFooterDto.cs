using Abp.Application.Services.Dto;
using AZMAdmin.CMS.Enums;

namespace AZMAdmin.CMS.Footers.Dto
{
    public class UpdateFooterDto : EntityDto<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string RedirectUrl { get; set; }
        public bool? IsActive { get; set; }
        public FooterEnum Type { get; set; }
    }
}