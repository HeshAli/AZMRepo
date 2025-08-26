using Abp.Domain.Entities.Auditing;
using AZMAdmin.CMS.Attachments;
using System.ComponentModel.DataAnnotations.Schema;

namespace AZMAdmin.CMS.HomeBanners
{
    public class HomeBanner : FullAuditedEntity<int>
    {
        public string NameAr {  get; set; }
        public string NameEn {  get; set; }
        public int? ImageId {  get; set; }
        public int? LogoId {  get; set; }
        public bool? IsActive {  get; set; }


        [ForeignKey(nameof(ImageId))]
        public virtual Attachment Image { get; set; }

        [ForeignKey(nameof(LogoId))]
        public virtual Attachment Logo { get; set; }
    }
}