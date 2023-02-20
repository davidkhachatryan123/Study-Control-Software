using System;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Shared;

namespace StudyControlSoftware_API.Interfaces.Base
{
	public interface IEducationBase<T> where T : class
	{
        public Task<TablesDataDto<T>> GetAllAsync(TableOptionsDto options);
        public Task<T> CreateAsync(T course);
        public Task<T?> UpdateAsync(int id, T course);
        public Task<int?> DeleteAsync(int id);
    }
}

