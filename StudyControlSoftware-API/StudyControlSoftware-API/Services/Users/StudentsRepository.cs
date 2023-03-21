using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Education;
using StudyControlSoftware_API.Dto.Shared;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Interfaces.Base;
using StudyControlSoftware_API.Interfaces.Users;
using StudyControlSoftware_API.Services.Base;

namespace StudyControlSoftware_API.Services.Users
{
    public class StudentsRepository : UsersBaseRepository<Student>, IUsersBase, IStudentsRepository
    {
        public StudentsRepository(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IMapper mapper) : base(context, userManager, passwordHasher, mapper) { }

        public async Task<TablesDataDto<UserDto>> GetAllAsync(TableOptionsDto options)
            => await base.FindAllAsync(options, x => x.StudentId);


        public async Task<FacultyDto?> GetFacultyAsync(string id)
        {
            var faculty =
                await _context.Faculties
                .Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.Students.FirstOrDefault(x => x.StudentId == id) != null);

            return _mapper.Map<FacultyDto>(faculty);
        }

        public async Task<FacultyDto?> SetFacultyAsync(string id, int facultyId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            var faculty = await _context.Faculties.FirstOrDefaultAsync(x => x.Id == facultyId);

            if (student != null && faculty != null)
            {
                student.FacultyId = faculty.Id;
                await _context.SaveChangesAsync();

                return _mapper.Map<FacultyDto>(faculty);
            }

            return null;
        }

        public async Task<FacultyDto?> DeleteFacultyAsync(string id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);

            if (student != null)
            {
                var faculty = await _context.Faculties.FirstOrDefaultAsync(x => x.Id == student.FacultyId);

                student.FacultyId = null;
                await _context.SaveChangesAsync();

                return _mapper.Map<FacultyDto>(faculty);
            }

            return null;
        }
    }
}

