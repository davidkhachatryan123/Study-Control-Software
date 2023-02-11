using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.Services
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;

        public UserAuthenticationRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<IdentityResult> Setup()
        {
            if (_context == null || _context!.Users.Any())
                return IdentityResult.Failed();

            ApplicationUser defaultUser = new ApplicationUser
            {
                UserName = _configuration["DefaultUser:UserName"],
                Email = _configuration["DefaultUser:Email"]
            };

            var result = await _userManager.CreateAsync(
                defaultUser,
                _configuration["DefaultUser:Password"]!);

            result = await _roleManager.CreateAsync(
                new IdentityRole(nameof(UserRoles.Admin)));
            result = await _roleManager.CreateAsync(
                new IdentityRole(nameof(UserRoles.Lecturer)));
            result = await _roleManager.CreateAsync(
                new IdentityRole(nameof(UserRoles.Student)));

            result = await _userManager.AddToRoleAsync(defaultUser, nameof(UserRoles.Admin));

            return result;
        }
    }
}
