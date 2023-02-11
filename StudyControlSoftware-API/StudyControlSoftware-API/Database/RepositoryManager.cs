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
        //private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private IUserAuthenticationRepository _userAuthenticationRepository;

        public RepositoryManager(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            //IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            //_mapper = mapper;
            _configuration = configuration;
        }

        public IUserAuthenticationRepository UserAuthentication
        {
            get
            {
                if (_userAuthenticationRepository is null)
                    _userAuthenticationRepository = new UserAuthenticationRepository(
                        _userManager, _roleManager, _context, _configuration);

                return _userAuthenticationRepository;
            }
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
