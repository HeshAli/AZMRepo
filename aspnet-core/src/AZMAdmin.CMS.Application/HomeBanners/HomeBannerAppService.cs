using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AutoMapper;
using AZMAdmin.CMS.HomeBanners.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.HomeBanners
{
    public class HomeBannerAppService : CMSAppServiceBase, IHomeBannerAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<HomeBanner, int> _repo;
        public HomeBannerAppService(IMapper mapper, IRepository<HomeBanner, int> Repository)  
        {
            _mapper = mapper;
            _repo = Repository;
        }
        public async Task ActivateDeactivateHomeBanner(int id)
        {
            var homeBanner = await _repo.FirstOrDefaultAsync(id);

            if(homeBanner is null)
                throw new UserFriendlyException(L("HomeBannerNotExist"));

            homeBanner.IsActive = !(homeBanner.IsActive ?? false);

            await _repo.UpdateAsync(homeBanner);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<HomeBannerDto> GetHomeBannerAsync(EntityDto<int> input)
        {
            var entity = await _repo.GetAsync(input.Id);
            return ObjectMapper.Map<HomeBannerDto>(entity);
        }

        public async Task<PagedResultDto<HomeBannerDto>> GetHomeBannerListAsync(PagedHomeBannerResultRequestDto input)
        {
            var query = _repo.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NameAr.Contains(input.Filter) || x.NameEn.Contains(input.Filter))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive).AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreationTime)
                .PageBy(input)                 // from Abp.Linq.Extensions
                .ToListAsync();

            var dtos = ObjectMapper.Map<List<HomeBannerDto>>(items);
            return new PagedResultDto<HomeBannerDto>(totalCount, dtos);
        }

        public async Task<HomeBannerDto> CreateHomeBannerAsync(CreateHomeBannerDto input)
        {
            var entity = ObjectMapper.Map<HomeBanner>(input);
            entity.IsActive ??= true;

            var id = await _repo.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            var created = await _repo.GetAsync(id);
            return ObjectMapper.Map<HomeBannerDto>(created);
        }

        public async Task<HomeBannerDto> UpdateHomeBannerAsync(UpdateHomeBannerDto input)
        {
            var entity = await _repo.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("HomeBannerNotExist")); // add this key in localization

            ObjectMapper.Map(input, entity);
            await _repo.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<HomeBannerDto>(entity);
        }

        public async Task DeleteHomeBannerAsync(EntityDto<int> input)
        {
            var entity = await _repo.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("HomeBannerNotExist"));

            await _repo.DeleteAsync(entity); // soft-delete respected by FullAuditedEntity if enabled
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}