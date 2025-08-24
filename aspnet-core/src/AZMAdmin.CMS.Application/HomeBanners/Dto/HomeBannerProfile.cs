using AutoMapper;

namespace AZMAdmin.CMS.HomeBanners.Dto
{
    public class HomeBannerProfile : Profile
    {
        public HomeBannerProfile()
        {
            CreateMap<HomeBanner, HomeBannerDto>();
            CreateMap<CreateHomeBannerDto, HomeBanner>();
            CreateMap<UpdateHomeBannerDto, HomeBanner>();
        }
    }
}