using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AZMAdmin.CMS.Courses.Dto;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        Task<CourseDto> GetCourseAsync(EntityDto<int> input);
        Task<PagedResultDto<CourseDto>> GetCourseListAsync(PagedCourseResultRequestDto input);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto input);
        Task<CourseDto> UpdateCourseAsync(UpdateCourseDto input);
        Task DeleteCourseAsync(EntityDto<int> input);
    }
}