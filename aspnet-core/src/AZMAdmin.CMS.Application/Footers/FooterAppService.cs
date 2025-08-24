using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AZMAdmin.CMS.Footers.Dto;
using System.Globalization;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Footers
{
    public class FooterAppService : AsyncCrudAppService<Footer, FooterDto, int, PagedFooterResultRequestDto, CreateFooterDto, UpdateFooterDto, GetFooterDto, DeleteFooterDto>, IFooterAppService
    {
        private readonly IRepository<Footer, int> _repository;
        public FooterAppService(IRepository<Footer, int> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task ActivateDeactivateFooter(int id)
        {
            var footer = await _repository.FirstOrDefaultAsync(id);

            if (footer is null)
                throw new UserFriendlyException(L("FooterNotExist", CultureInfo.CurrentCulture.Name));


            footer.IsActive = !(footer.IsActive ?? false);

            await _repository.UpdateAsync(footer);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}