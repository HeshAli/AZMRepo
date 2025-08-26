using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Portal.Dto
{
    public class ContentWithAttachmentDto : EntityDto<int>
    {
        public string DisplayName { get; set; } 
        public string DisplayDescription { get; set; } 
        public string RedirectUrl { get; set; }
        public bool? IsActive { get; set; }

        public string CategoryCode { get; set; }
        public int? AttachmentId { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
