using AutoMapper;

namespace AZMAdmin.CMS.Footers.Dto
{
    public class FooterProfile : Profile
    {
        public FooterProfile()
        {
            CreateMap<FooterDto, Footer>().ReverseMap();
        }
    }
}