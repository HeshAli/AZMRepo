using Abp.Domain.Entities.Auditing;

namespace AZMAdmin.CMS.CoursesDetails
{
    public class CourseDetails : FullAuditedEntity<int>
    {
        public string NameAr {  get; set; }
        public string NameEn { get; set; }
    }
}