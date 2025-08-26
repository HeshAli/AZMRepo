using Abp.Domain.Entities.Auditing;
using AZMAdmin.CMS.Attachments;
using AZMAdmin.CMS.ContentCategories;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey(nameof(AttachmentId))]
        public virtual Attachment Attachment { get; set; }

        [ForeignKey(nameof(ContentCategoryId))]
        public virtual ContentCategory ContentCategory { get; set; }
    }
}