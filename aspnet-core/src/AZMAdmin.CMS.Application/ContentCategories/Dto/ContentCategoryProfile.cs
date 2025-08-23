using AutoMapper;

namespace AZMAdmin.CMS.ContentCategories.Dto
{
    public class ContentCategoryProfile : Profile
    {
        public ContentCategoryProfile()
        {
            CreateMap<ContentCategoryDto, ContentCategory>().ReverseMap();
        }
    }
}