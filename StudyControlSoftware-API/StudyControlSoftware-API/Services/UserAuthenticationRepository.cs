using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using StudyControlSoftware_API.Controllers;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace StudyControlSoftware_API.Services
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly ApplicationContext _context;
        private readonly IEmailRepository _emailRepository;
        private readonly IAssetsRepository _assetsRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly LinkGenerator _linkGenerator;

        private ApplicationUser? _user;

        public UserAuthenticationRepository(
            ApplicationContext context,
            IEmailRepository emailRepository,
            IAssetsRepository assetsRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            LinkGenerator linkGenerator)
        {
            _context = context;
            _emailRepository = emailRepository;
            _assetsRepository = assetsRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _linkGenerator = linkGenerator;
        }

        public async Task<IdentityResult> SetupAsync()
        {
            /*if (_context == null || _context!.Users.Any())
                return IdentityResult.Failed();*/

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


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(defaultUser);
            var confirmationLink = ControllerLinkGeneratorExtensions.GetPathByAction(
                _linkGenerator,
                "ConfirmEmail",
                "AccountController"
                );

            /*var confirmationLink = Url.Action(
                "ConfirmEmail",
                nameof(AccountController),
                new ConfirmEmailDto { Email = defaultUser.Email!, Token = token }, Request.Scheme);*/

            _emailRepository.SendEmail(
                defaultUser.Email!,
                "StudyControlSoftware - Confirm Your Email",
                _assetsRepository.GetEmailConfirmationMessage(confirmationLink!));


            return result;
        }


        public async Task<bool> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            _user = await _userManager.FindByEmailAsync(confirmEmail.Email);

            return _user != null && await _userManager.ConfirmEmailAsync(_user, confirmEmail.Token) == IdentityResult.Success
                ? true : false;
        }


        public async Task<bool> ValidateUserAsync(UserLoginDto userLoginDto)
        {
            _user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            var result = _user != null && await _userManager.CheckPasswordAsync(_user, userLoginDto.Password);

            return result;
        }

        public async Task<string> Get2FACode() => await _userManager.GenerateTwoFactorTokenAsync(_user!, "Email");

        public async Task<bool> Validate2FACodeAsync(TwoFADto twoFA)
        {
            _user = await _userManager.FindByNameAsync(twoFA.UserName);

            return await _userManager.VerifyTwoFactorTokenAsync(
                _user!,
                _userManager.Options.Tokens.AuthenticatorTokenProvider,
                twoFA.Code);
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        public string GetEmail() => _user!.Email!;


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
                new Claim(ClaimTypes.Name, _user.UserName)
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
