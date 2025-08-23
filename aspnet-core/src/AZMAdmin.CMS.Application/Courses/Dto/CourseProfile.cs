using AutoMapper;

namespace AZMAdmin.CMS.Courses.Dto
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
        }
    }
}