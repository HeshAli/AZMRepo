 
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using AZMAdmin.CMS.Footers.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Globalization;
using System.Linq; 
using System.Threading.Tasks;
namespace AZMAdmin.CMS.Footers
{
    public class FooterAppService : CMSAppServiceBase, IFooterAppService
    {
        private readonly IRepository<Footer, int> _repository;
        public FooterAppService(IRepository<Footer, int> repository)  
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

        public async Task<FooterDto> GetFooterAsync(EntityDto<int> input)
        {
            var entity = await _repository.GetAsync(input.Id);
            if (entity is null)
                throw new UserFriendlyException(L("ContentNotExist", CultureInfo.CurrentCulture));
            return ObjectMapper.Map<FooterDto>(entity);
        }

        public async Task<PagedResultDto<FooterDto>> GetFooterListAsync(PagedFooterResultRequestDto input)
        {
            // Example filters (adjust to your DTO)

            var query = _repository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NameAr.Contains(input.Filter) || x.NameEn.Contains(input.Filter))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                .AsQueryable();
             
            var totalCount = await query.CountAsync();
            query = query.OrderByDescending(x => x.CreationTime);

            var items = await query.ToListAsync();

            var dtos = ObjectMapper.Map<List<FooterDto>>(items);
            return new PagedResultDto<FooterDto>(totalCount, dtos);
        }

        [Authorize] // require login (remove if public)
        public async Task<FooterDto> CreateFooterAsync(CreateFooterDto input)
        {

          
            var entity = ObjectMapper.Map<Footer>(input); 
            entity.IsActive = entity.IsActive ?? true;

            var id = await _repository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            var created = await _repository.GetAsync(id);
            return ObjectMapper.Map<FooterDto>(created);
        }

        [Authorize]
        public async Task<FooterDto> UpdateFooterAsync(UpdateFooterDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("ContentNotExist"));

            // map incoming fields
            ObjectMapper.Map(input, entity);

            await _repository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<FooterDto>(entity);
        }

        [Authorize]
        public async Task DeleteFooterAsync(EntityDto<int> input)
        {
            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("ContentNotExist"));

            await _repository.DeleteAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        // Helpers

    }
}
 