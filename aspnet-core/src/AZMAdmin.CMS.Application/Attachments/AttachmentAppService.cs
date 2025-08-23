using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using AZMAdmin.CMS.Attachments.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Attachments
{
    public class AttachmentAppService : AsyncCrudAppService<Attachment, AttachmentDto, int, PagedAttachmentResultRequestDto, CreateAttachmentDto, UpdateAttachmentDto, GetAttachmentDto, DeleteAttachmentDto>, IAttachmentAppService
    {
        private readonly IRepository<Attachment, int> _repository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public AttachmentAppService(IRepository<Attachment, int> repository, IWebHostEnvironment env, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _env = env;
            _mapper = mapper;
        }
        public async Task<AttachmentDto> UploadAttachment(IFormFile imageFile)
        {
            var uploadPath = Path.Combine(_env.WebRootPath, "attachments");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var originalName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            var extension = Path.GetExtension(imageFile.FileName);

            var safeName = string.Concat(originalName.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");

            var uniqueId = Guid.NewGuid().ToString("N").Substring(0, 8);
            var fileName = $"{safeName}_{uniqueId}{extension}";

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                Extension = extension,
                Path = $"/attachments/{fileName}",
                FullPath = filePath,
                IsActive = true
            };
            await _repository.InsertAsync(attachment);
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<Attachment, AttachmentDto>(attachment);
        }
        public async Task ActivateDeactivateAttachment(int id)
        {
            var attachment = await _repository.FirstOrDefaultAsync(id);

            if (attachment is null)
                throw new UserFriendlyException(L("AttachmentNotExist", new CultureInfo("ar")));


            attachment.IsActive = !(attachment.IsActive ?? false);

            await _repository.UpdateAsync(attachment);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        #region Not Implemented
        [RemoteService(false)]
        public override Task<AttachmentDto> CreateAsync(CreateAttachmentDto input)
        {
            return base.CreateAsync(input);
        }
        #endregion
    }
}