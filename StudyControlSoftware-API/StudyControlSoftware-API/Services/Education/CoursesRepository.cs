using System.Reflection;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Interfaces.Education;
using StudyControlSoftware_API.Dto.Education;
using Microsoft.EntityFrameworkCore;

namespace StudyControlSoftware_API.Services.Education
{
	public class CoursesRepository : ICoursesRepository
	{
        private readonly ApplicationContext _context;

		public CoursesRepository(
            ApplicationContext context)
		{
            _context = context;
		}

        public async Task<CoursesTableDto> GetAllAsync(TableOptionsDto options)
        {
            var propertyInfos =
                typeof(Course).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty =
                propertyInfos.FirstOrDefault(pi => pi.Name.Equals(options.Sort, StringComparison.InvariantCultureIgnoreCase));

            return new CoursesTableDto
            {
                Courses = (await _context.Courses.ToArrayAsync())
                        .OrderBy(x => options.OrderDirection == "asc" ? objectProperty!.GetValue(x, null) : null)
                        .OrderByDescending(x => options.OrderDirection == "desc" ? objectProperty!.GetValue(x, null) : null)
                        .Skip((options.Page - 1) * options.PageSize)
                        .Take(options.PageSize),

                TotalCount = _context.Courses.Count()
            };
        }

        public async Task<Course> CreateAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task<Course?> UpdateAsync(int id, Course course)
        {
            Course? updateCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (updateCourse != null)
            {
                updateCourse.Title = course.Title;
                updateCourse.Description = course.Description;

                await _context.SaveChangesAsync();

                return updateCourse;
            }
            else
                return null;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            Course? course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                return course.Id;
            }
            else
                return null;
        }
    }
}

