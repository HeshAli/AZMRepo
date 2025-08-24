using AutoMapper;
using AZMAdmin.CMS.ContentCategories;
using AZMAdmin.CMS.ContentCategories.Dto;

namespace AZMAdmin.CMS.Attachments.Dto
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<CreateAttachmentDto, Attachment>();
            CreateMap<UpdateAttachmentDto, Attachment>();
        }
    }
}