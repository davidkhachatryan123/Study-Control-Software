﻿using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;

namespace StudyControlSoftware_API.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> SetupAsync();

        Task<bool> IsEmailConfirmed(UserLoginDto userLogin);
        Task<bool> ConfirmEmail(ConfirmEmailDto confirmEmail);
        Task<string> GenerateEmailConfirmToken(UserLoginDto userLogin);

        Task<bool> ValidateUserAsync(UserLoginDto userLogin);
        Task<string> Get2FACode();
        Task<bool> Validate2FACodeAsync(TwoFADto twoFA);
        Task<string> CreateTokenAsync();

        string GetEmail();
    }
}
