using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Auth;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.Extensions
{
    public static class UrlHelperExtension
    {
        public static async Task<string?> GenerateConfirmationEmailLinkAsync(this IUrlHelper url, IRepositoryManager repositoryManager, string username)
        {
            var email =
                await repositoryManager.UserAuthentication.GetEmailAsync(username);
            var token =
                await repositoryManager.UserAuthentication.GenerateEmailConfirmToken(username);

            return email == null
                ? null
                : url.Action("ConfirmEmail",
                    "Account",
                    new ConfirmEmailDto
                    {
                        Email = email,
                        Token = token
                    }, "https");
        }
    }
}

