using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AZMAdmin.CMS.Courses.Dto;
using System.Globalization;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Courses
{
    public class CourseAppService : AsyncCrudAppService<Course, CourseDto, int, PagedCourseResultRequestDto, CreateCourseDto, UpdateCourseDto, GetCourseDto, DeleteCourseDto>, ICourseAppService
    {
        private readonly IRepository<Course, int> _repository;
        public CourseAppService(IRepository<Course, int> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}