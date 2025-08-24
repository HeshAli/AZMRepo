using Abp.Application.Services.Dto;
using AZMAdmin.CMS.Attachments.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Attachments
{
    public interface IAttachmentAppService
    {
        Task<AttachmentDto> UploadAttachment(IFormFile imageFile);
        Task ActivateDeactivateAttachment(int id);

        Task<AttachmentDto> GetAttachmentAsync(EntityDto<int> input); 
        Task<AttachmentDto> CreateAttachmentAsync(CreateAttachmentDto input);
        Task<AttachmentDto> UpdateAttachmentAsync(UpdateAttachmentDto input);
        Task DeleteAttachmentAsync(EntityDto<int> input);
    }
}