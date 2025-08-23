using AutoMapper;

namespace AZMAdmin.CMS.HomeBanners.Dto
{
    public class HomeBannerProfile : Profile
    {
        public HomeBannerProfile()
        {
            CreateMap<HomeBannerDto, HomeBanner>().ReverseMap();
        }
    }
}