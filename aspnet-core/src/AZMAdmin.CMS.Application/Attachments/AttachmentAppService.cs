using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using AZMAdmin.CMS.Attachments.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Attachments
{
    public class AttachmentAppService : CMSAppServiceBase , IAttachmentAppService
    {
        private readonly IRepository<Attachment, int> _repository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AttachmentAppService(IRepository<Attachment, int> repository, IWebHostEnvironment env, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _env = env;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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

            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            var attachment = new Attachment
            {
                Extension = extension,
                Path = $"{baseUrl}/attachments/{fileName}",
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
        public async Task<AttachmentDto> GetAttachmentAsync(EntityDto<int> input)
        {
            var entity = await _repository.GetAsync(input.Id);
            return ObjectMapper.Map<AttachmentDto>(entity);
        }

        public async Task<HomeBannerAttachmentsDto> GetHomeBannerAttachments(int? imageId = null, int? logoId = null)
        {
            var dto = new HomeBannerAttachmentsDto();

            if (imageId.HasValue)
            {
                var image = await _repository.FirstOrDefaultAsync(imageId.Value);
                if (image != null)
                    dto.Image = _mapper.Map<AttachmentDto>(image);
            }

            if (logoId.HasValue)
            {
                var logo = await _repository.FirstOrDefaultAsync(logoId.Value);
                if (logo != null)
                    dto.Logo = _mapper.Map<AttachmentDto>(logo);
            }

            return dto;
        }


        public async Task<AttachmentDto> CreateAttachmentAsync(CreateAttachmentDto input)
        {
            var entity = ObjectMapper.Map<Attachment>(input);
            entity.IsActive ??= true;

            var id = await _repository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            var created = await _repository.GetAsync(id);
            return ObjectMapper.Map<AttachmentDto>(created);
        }

        public async Task<AttachmentDto> UpdateAttachmentAsync(UpdateAttachmentDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("AttachmentNotExist")); // add this key to your localization

            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<AttachmentDto>(entity);
        }

        public async Task DeleteAttachmentAsync(EntityDto<int> input)
        {
            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("AttachmentNotExist"));

            await _repository.DeleteAsync(entity);     // respects soft delete in FullAuditedEntity
            await CurrentUnitOfWork.SaveChangesAsync();
        }

    }
}