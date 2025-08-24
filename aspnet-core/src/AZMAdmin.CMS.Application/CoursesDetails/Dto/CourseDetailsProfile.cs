using AutoMapper;

namespace AZMAdmin.CMS.CoursesDetails.Dto
{
    public class CourseDetailsProfile : Profile
    {
        public CourseDetailsProfile()
        {
            CreateMap<CourseDetails, CourseDetailsDto>();
            CreateMap<CreateCourseDetailsDto, CourseDetails>();
            CreateMap<UpdateCourseDetailsDto, CourseDetails>();
        }
    }
}