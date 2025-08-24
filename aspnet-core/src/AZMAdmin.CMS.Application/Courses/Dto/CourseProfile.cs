using AutoMapper;
using AZMAdmin.CMS.CoursesDetails;
using AZMAdmin.CMS.CoursesDetails.Dto;

namespace AZMAdmin.CMS.Courses.Dto
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        { 
            CreateMap<Course, CourseDto>();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();
        }
    }
}