using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AZMAdmin.CMS.CoursesDetails.Dto;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.CoursesDetails
{
    public interface ICourseDetailsAppService : IApplicationService
    {
        Task<CourseDetailsDto> GetCourseDetailsAsync(EntityDto<int> input);
        Task<PagedResultDto<CourseDetailsDto>> GetCourseDetailsListAsync(PagedCourseDetailsResultRequestDto input);
        Task<CourseDetailsDto> CreateCourseDetailsAsync(CreateCourseDetailsDto input);
        Task<CourseDetailsDto> UpdateCourseDetailsAsync(UpdateCourseDetailsDto input);
        Task DeleteCourseDetailsAsync(EntityDto<int> input);
        Task<PagedResultDto<CourseDetailsDto>> GetCourseDetailsByCourseIdAsync(PagedCourseDetailsResultRequestDto input, int? courseId);
    }

}