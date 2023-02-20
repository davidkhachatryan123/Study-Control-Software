using System;
using StudyControlSoftware_API.Database.Models;

namespace StudyControlSoftware_API.Dto.Education
{
	public class CoursesTableDto
	{
        public IEnumerable<Course>? Courses { get; set; }
        public int TotalCount { get; set; }
    }
}

