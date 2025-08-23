using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.Attachments.Dto
{
    public class UpdateAttachmentDto : EntityDto<int>
    {
        public bool? IsActive { get; set; }
    }
}