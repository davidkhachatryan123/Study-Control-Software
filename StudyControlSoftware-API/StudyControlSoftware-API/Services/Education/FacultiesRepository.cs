using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Education;
using StudyControlSoftware_API.Interfaces.Education;
using StudyControlSoftware_API.Services.Base;

namespace StudyControlSoftware_API.Services.Education
{
    public class FacultiesRepository<TEntity, TDto>
        : EducationBaseRepository<TEntity, TDto>, IFacultiesRepository
        where TEntity : class where TDto : class
    {
        private readonly ApplicationContext _context;

        public FacultiesRepository(
            ApplicationContext context,
            IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync(int id)
        {
            return await _context.FacultysCourses
                .Where(x => x.FacultyId == id)
                .Include(x => x.Course)
                .Select(x => new CourseDto
                {
                    Id = x.CourseId,
                    Title = x.Course.Title,
                    Description = x.Course.Description
                })
                .ToArrayAsync();
        }

        public async Task AddCoursesAsync(int id, IEnumerable<int> courses)
        {
            courses = courses.Distinct().ToList();

            foreach (var course in courses)
            {
                if (await _context.FacultysCourses.FirstOrDefaultAsync(x => x.FacultyId == id && x.CourseId == course) == null)
                    await _context.FacultysCourses.AddAsync(new FacultyCourse
                    {
                        FacultyId = id,
                        CourseId = course
                    });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCourseAsync(int id, int courseId)
        {
            var course = await _context.FacultysCourses.FirstOrDefaultAsync(x => x.FacultyId == id && x.CourseId == courseId);

            if (course != null)
            {
                _context.Remove(course);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}

