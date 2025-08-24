using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AZMAdmin.CMS.Courses.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AZMAdmin.CMS.Courses
{
    public class CourseAppService : CMSAppServiceBase, ICourseAppService
    {
        private readonly IRepository<Course, int> _repo;
        public CourseAppService(IRepository<Course, int> repository)  
        {
            _repo = repository;
        }

        public async Task<CourseDto> GetCourseAsync(EntityDto<int> input)
        {
            var entity = await _repo.GetAsync(input.Id);
            return ObjectMapper.Map<CourseDto>(entity);
        }

        public async Task<PagedResultDto<CourseDto>> GetCourseListAsync(PagedCourseResultRequestDto input)
        {
            var query = _repo.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NameAr.Contains(input.Filter) ||
                         x.NameEn.Contains(input.Filter) ||
                         x.DescriptionAr.Contains(input.Filter) ||
                         x.DescriptionEn.Contains(input.Filter)).AsQueryable();

            var totalCount = await query.CountAsync();

            // default sort by CreationTime desc (you can add safe dynamic sorting later)
            var items = await query
                .OrderByDescending(x => x.CreationTime)
                .PageBy(input)
                .ToListAsync();

            var dtos = ObjectMapper.Map<List<CourseDto>>(items);
            return new PagedResultDto<CourseDto>(totalCount, dtos);
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto input)
        {
            var entity = ObjectMapper.Map<Course>(input);

            var id = await _repo.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            var created = await _repo.GetAsync(id);
            return ObjectMapper.Map<CourseDto>(created);
        }

        public async Task<CourseDto> UpdateCourseAsync(UpdateCourseDto input)
        {
            var entity = await _repo.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("CourseNotExist",CultureInfo.CurrentCulture)); // add this key in localization

            ObjectMapper.Map(input, entity);
            await _repo.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<CourseDto>(entity);
        }

        public async Task DeleteCourseAsync(EntityDto<int> input)
        {
            var entity = await _repo.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new UserFriendlyException(L("CourseNotExist"));

            await _repo.DeleteAsync(entity); // FullAuditedEntity -> soft delete honored
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}