using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.Contents.Dto
{
    public class UpdateContentDto : EntityDto<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string RedirectUrl { get; set; }
        public bool? IsActive { get; set; }
    }
}