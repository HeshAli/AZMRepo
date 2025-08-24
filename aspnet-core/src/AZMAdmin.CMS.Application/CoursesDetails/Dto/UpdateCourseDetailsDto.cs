using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.CoursesDetails.Dto
{
    public class UpdateCourseDetailsDto : EntityDto<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
}