using Abp.Domain.Entities.Auditing;

namespace AZMAdmin.CMS.ContentCategories
{
    public class ContentCategory : FullAuditedEntity<int>
    {
        public string Code {  get; set; }
        public string NameAr {  get; set; }
        public string NameEn {  get; set; }
        public string DescriptionAr {  get; set; }
        public string DescriptionEn {  get; set; }
        public bool? IsActive { get; set; }
    }
}