namespace StudyControlSoftware_API.Dto.Shared
{
	public class TablesDataDto<T> where T : class
	{
        public IEnumerable<T>? Entities { get; set; }
        public int TotalCount { get; set; }
    }
}

