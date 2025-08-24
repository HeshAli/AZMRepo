using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.Contents.Dto
{
    public class PagedContentResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
        public bool? IsActive { get; set; }
        public int? ContentCategoryId { get; set; }
        public string CategoryCode { get; set; }
    }
}