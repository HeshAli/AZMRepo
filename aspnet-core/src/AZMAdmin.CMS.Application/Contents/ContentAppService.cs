using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AZMAdmin.CMS.Contents.Dto;
using System.Globalization;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Contents
{
    public class ContentAppService : AsyncCrudAppService<Content, ContentDto, int, PagedContentResultRequestDto, CreateContentDto, UpdateContentDto, GetContentDto, DeleteContentDto> , IContentAppService
    {
        private readonly IRepository<Content, int> _repository;
        public ContentAppService(IRepository<Content, int> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task ActivateDeactivateContent(int id)
        {
            var content = await _repository.FirstOrDefaultAsync(id);

            if (content is null)
                throw new UserFriendlyException(L("ContentNotExist", new CultureInfo("ar")));


            content.IsActive = !(content.IsActive ?? false);

            await _repository.UpdateAsync(content);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}