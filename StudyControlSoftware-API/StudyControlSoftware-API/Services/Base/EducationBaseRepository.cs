using StudyControlSoftware_API.Database;
using System.Reflection;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Shared;
using StudyControlSoftware_API.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StudyControlSoftware_API.Database.Base;

namespace StudyControlSoftware_API.Services.Base
{
	public class EducationBaseRepository<TEntity, TDto>
        : IEducationBaseRepository<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public EducationBaseRepository(
            ApplicationContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TablesDataDto<TDto>> GetAllAsync(TableOptionsDto options)
        {
            var propertyInfos =
                typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty =
                propertyInfos.FirstOrDefault(
                    pi => pi.Name.Equals(
                        options.Sort, StringComparison.InvariantCultureIgnoreCase));

            return new TablesDataDto<TDto>
            {
                Entities = (await _context.Set<TEntity>().ToArrayAsync())
                        .OrderBy(x => options.OrderDirection == "asc" ? objectProperty!.GetValue(x, null) : null)
                        .OrderByDescending(x => options.OrderDirection == "desc" ? objectProperty!.GetValue(x, null) : null)
                        .Skip((options.Page - 1) * options.PageSize)
                        .Take(options.PageSize)
                        .Select(e => _mapper.Map<TDto>(e)),

                TotalCount = _context.Set<TEntity>().Count()
            };
        }

        public async Task<TDto> CreateAsync(TDto data)
        {
            _context.Set<TEntity>().Add(
                _mapper.Map<TEntity>(data));

            await _context.SaveChangesAsync();

            return data;
        }

        public async Task<TDto?> UpdateAsync(int id, TDto data)
        {
            var propertyInfos =
                typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty =
                propertyInfos.FirstOrDefault(
            pi => pi.Name.Equals(
                        "id", StringComparison.InvariantCultureIgnoreCase));

            var updateEntity = (await _context.Set<TEntity>().ToListAsync())
                .FirstOrDefault(c =>
                Convert.ToInt32(objectProperty!.GetValue(c, null)) == id);

            if (updateEntity != null)
            {
                updateEntity = _mapper.Map(data, updateEntity);

                await _context.SaveChangesAsync();

                return _mapper.Map<TDto>(updateEntity);
            }
            else
                return null;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var propertyInfos =
                typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty =
                propertyInfos.FirstOrDefault(
            pi => pi.Name.Equals(
                        "id", StringComparison.InvariantCultureIgnoreCase));

            var entity = (await _context.Set<TEntity>().ToListAsync())
                .FirstOrDefault(c =>
                Convert.ToInt32(objectProperty!.GetValue(c, null)) == id);

            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();

                return int.Parse(objectProperty!.GetValue(entity, null)!.ToString()!);
            }
            else
                return null;
        }
    }
}

