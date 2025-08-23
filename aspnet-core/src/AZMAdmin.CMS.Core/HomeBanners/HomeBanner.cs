using Abp.Domain.Entities.Auditing;

namespace AZMAdmin.CMS.HomeBanners
{
    public class HomeBanner : FullAuditedEntity<int>
    {
        public string NameAr {  get; set; }
        public string NameEn {  get; set; }
        public int? ImageId {  get; set; }
        public int? LogoId {  get; set; }
        public bool? IsActive {  get; set; }
    }
}