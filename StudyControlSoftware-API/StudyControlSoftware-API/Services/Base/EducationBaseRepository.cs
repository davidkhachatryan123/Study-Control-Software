using StudyControlSoftware_API.Database;
using System.Reflection;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Shared;
using StudyControlSoftware_API.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace StudyControlSoftware_API.Services.Base
{
	public class EducationBaseRepository<T> : IEducationBase<T> where T : class
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

        public async Task<TablesDataDto<T>> GetAllAsync(TableOptionsDto options)
        {
            var propertyInfos =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty =
                propertyInfos.FirstOrDefault(
                    pi => pi.Name.Equals(
                        options.Sort, StringComparison.InvariantCultureIgnoreCase));

            return new TablesDataDto<T>
            {
                Entities = (await _context.Set<T>().ToArrayAsync())
                        .OrderBy(x => options.OrderDirection == "asc" ? objectProperty!.GetValue(x, null) : null)
                        .OrderByDescending(x => options.OrderDirection == "desc" ? objectProperty!.GetValue(x, null) : null)
                        .Skip((options.Page - 1) * options.PageSize)
                        .Take(options.PageSize),

                TotalCount = _context.Courses.Count()
            };
        }

        public async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T?> UpdateAsync(int id, T entity)
        {
            var propertyInfos =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty =
                propertyInfos.FirstOrDefault(
            pi => pi.Name.Equals(
                        "id", StringComparison.InvariantCultureIgnoreCase));

            var updateEntity = (await _context.Set<T>().ToListAsync())
                .FirstOrDefault(c =>
                Convert.ToInt32(objectProperty!.GetValue(c, null)) == id);

            if (updateEntity != null)
            {
                var a = _mapper.Map(updateEntity, entity);

                //_context.Set<T>().Update(updateEntity);
                await _context.SaveChangesAsync();

                return updateEntity;
            }
            else
                return null;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var propertyInfos =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty =
                propertyInfos.FirstOrDefault(
            pi => pi.Name.Equals(
                        "id", StringComparison.InvariantCultureIgnoreCase));

            var entity = (await _context.Set<T>().ToListAsync())
                .FirstOrDefault(c =>
                Convert.ToInt32(objectProperty!.GetValue(c, null)) == id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();

                return int.Parse(objectProperty!.GetValue(entity, null)!.ToString()!);
            }
            else
                return null;
        }
    }
}

