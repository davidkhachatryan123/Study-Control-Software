using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Interfaces;
using StudyControlSoftware_API.Services;

namespace StudyControlSoftware_API.Database
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
