using Abp.Application.Services.Dto;
using AZMAdmin.CMS.Enums;

namespace AZMAdmin.CMS.ContentCategories.Dto
{
    public class UpdateContentCategoryDto : EntityDto<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public bool? IsActive { get; set; }
        public ContentEnum Type { get; set; }
    }
}