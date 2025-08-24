using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.Courses.Dto
{
    public class PagedCourseResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
    }
}