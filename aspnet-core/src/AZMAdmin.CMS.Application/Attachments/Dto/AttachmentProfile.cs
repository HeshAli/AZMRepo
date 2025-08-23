using AutoMapper;

namespace AZMAdmin.CMS.Attachments.Dto
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<AttachmentDto, Attachment>().ReverseMap();
        }
    }
}