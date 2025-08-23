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
    }
}