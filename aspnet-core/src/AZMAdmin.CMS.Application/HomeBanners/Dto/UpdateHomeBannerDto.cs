using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.HomeBanners.Dto
{
    public class UpdateHomeBannerDto : EntityDto<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int? ImageId { get; set; }
        public int? LogoId { get; set; }
        public bool? IsActive { get; set; }
    }
}