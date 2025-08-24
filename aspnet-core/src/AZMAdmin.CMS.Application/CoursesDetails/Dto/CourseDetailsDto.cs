using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.CoursesDetails.Dto
{
    public class CourseDetailsDto : EntityDto<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
}