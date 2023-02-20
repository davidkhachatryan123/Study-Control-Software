using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Shared;

namespace StudyControlSoftware_API.Interfaces.Base
{
    public interface IEducationBaseRepository<TEntity, TDto> where TEntity : class where TDto : class
	{
        public Task<TablesDataDto<TDto>> GetAllAsync(TableOptionsDto options);
        public Task<TDto> CreateAsync(TDto data);
        public Task<TDto?> UpdateAsync(int id, TDto data);
        public Task<int?> DeleteAsync(int id);
    }
}

