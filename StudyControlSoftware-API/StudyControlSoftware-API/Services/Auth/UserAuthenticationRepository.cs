using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Auth;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudyControlSoftware_API.Services.Auth
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private ApplicationUser? _user;

        public UserAuthenticationRepository(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SetupAsync()
        {
            await _context.Database.MigrateAsync();

            if (_context == null || _context!.Users.Any())
                return IdentityResult.Failed();

            ApplicationUser defaultUser = new ApplicationUser
            {
                UserName = _configuration["DefaultUser:UserName"],
                Email = _configuration["DefaultUser:Email"],
                TwoFactorEnabled = true
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


        public async Task<bool> IsEmailConfirmed(UserLoginDto userLoginDto)
        {
            _user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            return _user != null && _user.EmailConfirmed;
        }

        public async Task<bool> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            _user = await _userManager.FindByEmailAsync(confirmEmail.Email);

            return _user != null && await _userManager.ConfirmEmailAsync(_user, confirmEmail.Token) == IdentityResult.Success
                ? true : false;
        }

        public async Task<string?> GenerateEmailConfirmToken(string email)
        {
            _user = await _userManager.FindByEmailAsync(email);

            return _user == null
                ? null
                : await _userManager.GenerateEmailConfirmationTokenAsync(_user!);
        }


        public async Task<bool> IsUserExists(UserLoginDto userLogin)
             => await _userManager.FindByNameAsync(userLogin.UserName) != null ? true : false;

        public async Task<bool> ValidateUserAsync(UserLoginDto userLoginDto)
        {
            _user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            return _user != null
                && await _userManager.CheckPasswordAsync(_user, userLoginDto.Password)
                && await _userManager.IsEmailConfirmedAsync(_user);
        }

        public async Task<string> Get2FACode()
            => await _userManager.GenerateTwoFactorTokenAsync(_user!, TokenOptions.DefaultEmailProvider);

        public async Task<bool> Validate2FACodeAsync(TwoFADto twoFA)
        {
            _user = await _userManager.FindByNameAsync(twoFA.Username);

            return _user != null
                && await _userManager.VerifyTwoFactorTokenAsync(
                _user!,
                TokenOptions.DefaultEmailProvider,
                twoFA.Code);
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        public async Task<string?> GetEmailAsync(string username)
        {
            _user = await _userManager.FindByNameAsync(username);

            return _user == null
                ? null
                : await _userManager.GetEmailAsync(_user);
        }

        public async Task<string?> GetRole(string email)
        {
            return (await this.GetClaims()).FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }


        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]!);

            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user!.UserName!)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}
