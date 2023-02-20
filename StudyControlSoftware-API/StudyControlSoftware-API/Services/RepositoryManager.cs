using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Education;
using StudyControlSoftware_API.Interfaces;
using StudyControlSoftware_API.Interfaces.Auth;
using StudyControlSoftware_API.Interfaces.Base;
using StudyControlSoftware_API.Interfaces.Education;
using StudyControlSoftware_API.Interfaces.Shared;
using StudyControlSoftware_API.Services.Auth;
using StudyControlSoftware_API.Services.Education;
using StudyControlSoftware_API.Services.Shared;
using StudyControlSoftware_API.Services.Users;

namespace StudyControlSoftware_API.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RepositoryManager> _logger;

        private IUserAuthenticationRepository _userAuthenticationRepository;

        private IUsersBase _adminsRepository;
        private IUsersBase _lecturersRepository;
        private IUsersBase _studentsRepository;

        private IEducationBaseRepository<Course, CourseDto> _coursesRepository;
        private IEducationBaseRepository<Faculty, FacultyDto> _facultiesRepository;

        private IEmailRepository _emailRepository;
        private IAssetsRepository _assetsRepository;

        public RepositoryManager(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<RepositoryManager> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public IUserAuthenticationRepository UserAuthentication
        {
            get
            {
                _userAuthenticationRepository ??= new UserAuthenticationRepository(
                        _context, _userManager, _roleManager, _configuration);

                return _userAuthenticationRepository;
            }
        }


        public IUsersBase Admins
        {
            get
            {
                _adminsRepository ??= new AdminsRepository(_userManager, _passwordHasher, _mapper);

                return _adminsRepository;
            }
        }

        public IUsersBase Lecturers
        {
            get
            {
                _lecturersRepository ??= new LecturersRepository(_context, _userManager, _passwordHasher, _mapper);

                return _lecturersRepository;
            }
        }

        public IUsersBase Students
        {
            get
            {
                _studentsRepository ??= new StudentsRepository(_context, _userManager, _passwordHasher, _mapper);

                return _studentsRepository;
            }
        }


        public IEducationBaseRepository<Course, CourseDto> Courses
        {
            get
            {
                _coursesRepository ??= new CoursesRepository(_context, _mapper);

                return _coursesRepository;
            }
        }

        public IEducationBaseRepository<Faculty, FacultyDto> Faculties
        {
            get
            {
                _facultiesRepository ??= new FacultiesRepository(_context, _mapper);

                return _facultiesRepository;
            }
        }


        public IEmailRepository Email
        {
            get
            {
                _emailRepository ??= new EmailRepository(_logger, _configuration);

                return _emailRepository;
            }
        }

        public IAssetsRepository Assets
        {
            get
            {
                _assetsRepository ??= new AssetsRepository(_configuration);

                return _assetsRepository;
            }
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
