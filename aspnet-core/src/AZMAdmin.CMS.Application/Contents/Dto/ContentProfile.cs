using AutoMapper;
using AZMAdmin.CMS.Courses;
using AZMAdmin.CMS.Courses.Dto;

namespace AZMAdmin.CMS.Contents.Dto
{
    public class ContentProfile : Profile
    {
        public ContentProfile()
        { 
            CreateMap<Content, ContentDto>();
            CreateMap<CreateContentDto, Content>();
            CreateMap<UpdateContentDto, Content>();
        }
    }
}