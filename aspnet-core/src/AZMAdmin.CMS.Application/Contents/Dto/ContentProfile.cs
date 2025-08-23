using AutoMapper;

namespace AZMAdmin.CMS.Contents.Dto
{
    public class ContentProfile : Profile
    {
        public ContentProfile()
        {
            CreateMap<ContentDto, Content>().ReverseMap();
        }
    }
}