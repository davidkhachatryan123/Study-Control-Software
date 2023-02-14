using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Interfaces;
using StudyControlSoftware_API.Interfaces.Auth;
using StudyControlSoftware_API.Interfaces.Shared;
using StudyControlSoftware_API.Interfaces.Users;
using StudyControlSoftware_API.Services.Auth;
using StudyControlSoftware_API.Services.Shared;
using StudyControlSoftware_API.Services.Users;

namespace StudyControlSoftware_API.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RepositoryManager> _logger;

        private IUserAuthenticationRepository _userAuthenticationRepository;
        private IAdminsRepository _adminsRepository;
        private IEmailRepository _emailRepository;
        private IAssetsRepository _assetsRepository;

        public RepositoryManager(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<RepositoryManager> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public IAdminsRepository Admins
        {
            get
            {
                _adminsRepository ??= new AdminsRepository(_userManager);

                return _adminsRepository;
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
