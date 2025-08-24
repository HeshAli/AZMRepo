using Abp.Domain.Entities.Auditing;

namespace AZMAdmin.CMS.Courses
{
    public class Course : FullAuditedEntity<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string RedirectUrl { get; set; }
    }
}