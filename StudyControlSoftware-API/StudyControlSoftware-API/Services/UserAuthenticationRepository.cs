﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudyControlSoftware_API.Services
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

        public async Task<string> GenerateEmailConfirmToken(UserLoginDto userLogin)
        {
            _user = await _userManager.FindByNameAsync(userLogin.UserName);

            return await _userManager.GenerateEmailConfirmationTokenAsync(_user!);
        }


        public async Task<bool> ValidateUserAsync(UserLoginDto userLoginDto)
        {
            _user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            var result = 
                _user != null
                && await _userManager.CheckPasswordAsync(_user, userLoginDto.Password)
                && await _userManager.IsEmailConfirmedAsync(_user);

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