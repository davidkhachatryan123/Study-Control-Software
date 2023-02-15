using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Dto.Auth;
using StudyControlSoftware_API.Extensions;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public AccountController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [Route("2FA")]
        [HttpPost]
        public async Task<IActionResult> Login2FA([FromBody] TwoFADto twoFA)
        {
            return !await _repositoryManager.UserAuthentication.Validate2FACodeAsync(twoFA)
                ? Unauthorized()
                : Ok(new { Token = await _repositoryManager.UserAuthentication.CreateTokenAsync() });
        }

        [AllowAnonymous]
        [Route("ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto confirmEmailDto)
        {
            return !await _repositoryManager.UserAuthentication.ConfirmEmail(confirmEmailDto)
                ? Unauthorized()
                : Content(_repositoryManager.Assets.GetRedirectToLogin(), "text/html");
        }
    }
}
