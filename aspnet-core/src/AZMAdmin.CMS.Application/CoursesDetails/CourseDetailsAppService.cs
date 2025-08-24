using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AZMAdmin.CMS.CoursesDetails.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.CoursesDetails
{
    public class CourseDetailsAppService : CMSAppServiceBase, ICourseDetailsAppService
    {
        private readonly IRepository<CourseDetails, int> _repo;

        public CourseDetailsAppService(IRepository<CourseDetails, int> repo)
        {
            _repo = repo; 
        }

        public async Task<CourseDetailsDto> GetCourseDetailsAsync(EntityDto<int> input)
        {
            var entity = await _repo.GetAsync(input.Id);
            return ObjectMapper.Map<CourseDetailsDto>(entity);
        }

        public async Task<PagedResultDto<CourseDetailsDto>> GetCourseDetailsListAsync(PagedCourseDetailsResultRequestDto input)
        {
            var query = _repo.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NameAr.Contains(input.Filter) || x.NameEn.Contains(input.Filter)).AsQueryable();

            var totalCount = await query.CountAsync();

            // default sort by newest
            var items = await query
                .OrderByDescending(x => x.CreationTime)
                .PageBy(input)                 // uses Skip/Take from PagedAndSortedResultRequestDto
                .ToListAsync();

            var dtos = ObjectMapper.Map<List<CourseDetailsDto>>(items);
            return new PagedResultDto<CourseDetailsDto>(totalCount, dtos);
        }

        public async Task<CourseDetailsDto> CreateCourseDetailsAsync(CreateCourseDetailsDto input)
        {
            var entity = ObjectMapper.Map<CourseDetails>(input);

            var id = await _repo.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            var created = await _repo.GetAsync(id);
            return ObjectMapper.Map<CourseDetailsDto>(created);
        }

        public async Task<CourseDetailsDto> UpdateCourseDetailsAsync(UpdateCourseDetailsDto input)
        {
            var entity = await _repo.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("CourseDetailsNotExist")); // add this key in localization

            ObjectMapper.Map(input, entity);
            await _repo.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<CourseDetailsDto>(entity);
        }

        public async Task DeleteCourseDetailsAsync(EntityDto<int> input)
        {
            var entity = await _repo.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("CourseDetailsNotExist"));

            await _repo.DeleteAsync(entity); // soft-delete honored if enabled via FullAuditedEntity
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}