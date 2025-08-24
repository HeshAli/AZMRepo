using Abp.Application.Services.Dto;

namespace AZMAdmin.CMS.ContentCategories.Dto
{
    public class PagedContentCategoryResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
        public bool? IsActive { get; set; }
    }
}