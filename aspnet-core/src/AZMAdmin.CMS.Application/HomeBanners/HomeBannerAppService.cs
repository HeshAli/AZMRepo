using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using AZMAdmin.CMS.HomeBanners.Dto;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.HomeBanners
{
    public class HomeBannerAppService : AsyncCrudAppService<HomeBanner, HomeBannerDto, int, PagedHomeBannerResultRequestDto, CreateHomeBannerDto, UpdateHomeBannerDto, GetHomeBannerDto, DeleteHomeBannerDto>, IHomeBannerAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<HomeBanner, int> _repository;
        public HomeBannerAppService(IMapper mapper, IRepository<HomeBanner, int> Repository) : base(Repository)
        {
            _mapper = mapper;
            _repository = Repository;
        }
        public async Task ActivateDeactivateHomeBanner(int id)
        {
            var homeBanner = await _repository.FirstOrDefaultAsync(id);

            if(homeBanner is null)
                throw new UserFriendlyException(L("HomeBannerNotExist"));

            homeBanner.IsActive = !(homeBanner.IsActive ?? false);

            await _repository.UpdateAsync(homeBanner);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}