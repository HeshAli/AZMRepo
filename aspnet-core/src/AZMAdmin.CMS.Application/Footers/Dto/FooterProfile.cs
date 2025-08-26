using AutoMapper;

namespace AZMAdmin.CMS.Footers.Dto
{
    public class FooterProfile : Profile
    {
        public FooterProfile()
        {
            CreateMap<Footer, FooterDto>();
            CreateMap<CreateFooterDto, Footer>();
            CreateMap<UpdateFooterDto, Footer>();
        }
    }
}