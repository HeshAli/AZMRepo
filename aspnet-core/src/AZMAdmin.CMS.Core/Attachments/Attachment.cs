using Abp.Domain.Entities.Auditing;

namespace AZMAdmin.CMS.Attachments
{
    public class Attachment : FullAuditedEntity<int>
    {
        public string Extension { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public bool? IsActive { get; set; }
    }
}