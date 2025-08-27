using Abp.Domain.Entities.Auditing;
using AZMAdmin.CMS.Courses;
using System.ComponentModel.DataAnnotations.Schema;

namespace AZMAdmin.CMS.CoursesDetails
{
    public class CourseDetails : FullAuditedEntity<int>
    {
        public int CourseId { get; set; }
        public string NameAr {  get; set; }
        public string NameEn { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }
    }
}