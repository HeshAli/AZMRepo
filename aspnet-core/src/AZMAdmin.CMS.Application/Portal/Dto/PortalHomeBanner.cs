using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Portal.Dto
{
    public class PortalHomeBanner : EntityDto<int>
    {
        public string DisplayName { get; set; }
        public string? ImageURL { get; set; }
        public string? LogoURL { get; set; }
    }
}
