using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Dto.Auth;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Extensions;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public AccountController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            string email = (await _repositoryManager.UserAuthentication.GetEmailAsync(user.UserName))!;

            if (await _repositoryManager.UserAuthentication.ValidateUserAsync(user))
            {
                _repositoryManager.Email.SendEmail(
                    email,
                    "StudyControlSoftware - 2FA",
                    _repositoryManager.Assets.Get2FAMessage(
                        await _repositoryManager.UserAuthentication.Get2FACode()));

                return Ok();
            }
            else if (await _repositoryManager.UserAuthentication.IsUserExists(user)
                && !await _repositoryManager.UserAuthentication.IsEmailConfirmed(user))
            {
                var confirmationLink = await Url.GenerateConfirmationEmailLinkAsync(
                    _repositoryManager, user.UserName);

                if (confirmationLink == null)
                    return Unauthorized("Confirmation Email doesn't generated!");

                _repositoryManager.Email.SendEmail(
                    email,
                    "StudyControlSoftware - Confirmation Email",
                    _repositoryManager.Assets.GetEmailConfirmationMessage(
                        confirmationLink));

                return Unauthorized("Email doesn't confirmed! Confirmation email sended to Your Email!");
            }
            else
                return Unauthorized();
        }

        [Route("2FA")]
        [HttpPost]
        public async Task<IActionResult> Login2FA([FromBody] TwoFADto twoFA)
        {
            return !await _repositoryManager.UserAuthentication.Validate2FACodeAsync(twoFA)
                ? Unauthorized()
                : Ok(new { Token = await _repositoryManager.UserAuthentication.CreateTokenAsync() });
        }

        [Route("ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto confirmEmailDto)
        {
            return !await _repositoryManager.UserAuthentication.ConfirmEmail(confirmEmailDto)
                ? Unauthorized()
                : Content(_repositoryManager.Assets.GetRedirectToLogin(), "text/html");
        }

        [Authorize(Policy = nameof(UserRoles.Admin))]
        [Route("SendConfirmEmailMessage")]
        [HttpPost]
        public async Task<IActionResult> SendConfirmEmailMessage([FromBody] string username)
        {
            var email = await _repositoryManager.UserAuthentication.GetEmailAsync(username);
            var confirmationLink = await Url.GenerateConfirmationEmailLinkAsync(
                    _repositoryManager, username);

            if (email == null || confirmationLink == null) return BadRequest();

            var result = _repositoryManager.Email.SendEmail(
                email,
                "StudyControlSoftware - Confirmation Email",
                _repositoryManager.Assets.GetEmailConfirmationMessage(
                    confirmationLink));

            return !result
                ? BadRequest()
                : Ok();
        }
    }
}
