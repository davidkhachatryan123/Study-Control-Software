using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using StudyControlSoftware_API.Dto;
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
            if (await _repositoryManager.UserAuthentication.ValidateUserAsync(user))
            {
                _repositoryManager.Email.SendEmail(
                    _repositoryManager.UserAuthentication.GetEmail(),
                    "StudyControlSoftware - 2FA",
                    _repositoryManager.Assets.Get2FAMessage(
                        await _repositoryManager.UserAuthentication.Get2FACode()));

                return Ok();
            }
            else if (!await _repositoryManager.UserAuthentication.IsEmailConfirmed(user) != null)
            {
                var confirmationLink = Url.Action(
                    "ConfirmEmail",
                    "Account",
                    new ConfirmEmailDto
                    {
                        Email = _repositoryManager.UserAuthentication.GetEmail(),
                        Token = await _repositoryManager.UserAuthentication.GenerateEmailConfirmToken(user)
                    },
                    Request.Scheme);

                _repositoryManager.Email.SendEmail(
                    _repositoryManager.UserAuthentication.GetEmail(),
                    "StudyControlSoftware - Confirmation Email",
                    _repositoryManager.Assets.GetEmailConfirmationMessage(
                        confirmationLink!));

                return Unauthorized("Email doesn't confirmed! Confirmation email send to Your Email!");
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
    }
}
