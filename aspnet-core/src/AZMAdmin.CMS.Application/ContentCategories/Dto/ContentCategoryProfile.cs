using AutoMapper;
using AZMAdmin.CMS.Contents;
using AZMAdmin.CMS.Contents.Dto;

namespace AZMAdmin.CMS.ContentCategories.Dto
{
    public class ContentCategoryProfile : Profile
    {
        public ContentCategoryProfile()
        { 
            CreateMap<ContentCategory, ContentCategoryDto>();
            CreateMap<CreateContentCategoryDto, ContentCategory>();
            CreateMap<UpdateContentCategoryDto, ContentCategory>();
        }
    }
}