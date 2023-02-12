using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            return !await _repositoryManager.UserAuthentication.ConfirmEmail(confirmEmail)
                ? Unauthorized()
                : Content(_repositoryManager.Assets.GetRedirectToLogin(), "text/html");
        }
    }
}
