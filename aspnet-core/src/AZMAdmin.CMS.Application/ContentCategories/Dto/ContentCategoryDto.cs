using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.ContentCategories.Dto
{
    public class ContentCategoryDto : EntityDto<int>
    {
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public bool? IsActive { get; set; }
    }
}