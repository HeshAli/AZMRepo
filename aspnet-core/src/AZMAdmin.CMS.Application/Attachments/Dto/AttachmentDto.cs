using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.Attachments.Dto
{
    public class AttachmentDto : EntityDto<int>
    {
        public string Extension { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public bool? IsActive { get; set; }
    }
}