using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Education;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Interfaces.Education;
using StudyControlSoftware_API.Services.Base;

namespace StudyControlSoftware_API.Services.Education
{
    public class CoursesRepository<TEntity, TDto>
        : EducationBaseRepository<TEntity, TDto>, ICoursesRepository
        where TEntity : class where TDto : class
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CoursesRepository(
            ApplicationContext context,
            IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LecturerDto?> GetLecturerAsync(int id)
        {
            var lecturer = 
                _context.Lecturers
                .Include(x => x.Courses)
                .FirstOrDefault(x => x.Courses.FirstOrDefault(x => x.Id == id) != null);

            return _mapper.Map<LecturerDto>(lecturer);
        }

        public async Task<LecturerDto?> SetLecturerAsync(int id, string lecturerId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.LecturerId == lecturerId);

            if (course != null && lecturer != null)
            {
                course.LecturerId = lecturer.LecturerId;
                await _context.SaveChangesAsync();

                return _mapper.Map<LecturerDto>(lecturer);
            }

            return null;
        }

        public async Task<LecturerDto?> DeleteLecturerAsync(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

            if (course != null)
            {
                var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.LecturerId == course.LecturerId);

                course.LecturerId = null;
                await _context.SaveChangesAsync();

                return _mapper.Map<LecturerDto>(lecturer);
            }
                
            return null;
        }
    }
}

