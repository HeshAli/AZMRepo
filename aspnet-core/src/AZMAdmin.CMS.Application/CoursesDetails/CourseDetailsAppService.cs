using Abp.Application.Services;
using Abp.Domain.Repositories;
using AZMAdmin.CMS.CoursesDetails.Dto;

namespace AZMAdmin.CMS.CoursesDetails
{
    public class CourseDetailsAppService : AsyncCrudAppService<CourseDetails, CourseDetailsDto, int, PagedCourseDetailsResultRequestDto, CreateCourseDetailsDto, UpdateCourseDetailsDto, GetCourseDetailsDto, DeleteCourseDetailsDto>, ICourseDetailsAppService
    {
        private readonly IRepository<CourseDetails, int> _repository;
        public CourseDetailsAppService(IRepository<CourseDetails, int> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}