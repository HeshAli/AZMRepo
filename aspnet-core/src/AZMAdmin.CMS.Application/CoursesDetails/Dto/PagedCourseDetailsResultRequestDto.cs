using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.CoursesDetails.Dto
{
    public class PagedCourseDetailsResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
    }
}