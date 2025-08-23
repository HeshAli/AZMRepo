using Abp.Domain.Entities.Auditing;

namespace AZMAdmin.CMS.Footers
{
    public class Footer : FullAuditedEntity<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string RedirectUrl { get; set; }
        public bool? IsActive { get; set; }
    }
}