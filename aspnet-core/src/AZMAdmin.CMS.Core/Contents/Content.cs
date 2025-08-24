using Abp.Domain.Entities.Auditing;

namespace AZMAdmin.CMS.Contents
{
    public class Content : FullAuditedEntity<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string RedirectUrl { get; set; }
        public bool? IsActive { get; set; }
        public int? ContentCategoryId { get; set; }
        public int? AttachmentId { get; set; }
    }
}