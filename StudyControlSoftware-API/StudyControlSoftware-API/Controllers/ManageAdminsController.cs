using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Extensions;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.Controllers
{
    [Authorize(Policy = nameof(UserRoles.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class ManageAdminsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public ManageAdminsController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TableOptionsDto options)
        {
            return Ok(await _repositoryManager.Admins.GetAllAsync(options));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegisterDto user)
        {
            var _user = await _repositoryManager.Admins.CreateAsync(user);

            var confirmationLink = await Url.GenerateConfirmationEmailLinkAsync(
                    _repositoryManager, user.UserName);

            if (confirmationLink == null) return BadRequest();

            _repositoryManager.Email.SendEmail(
                (await _repositoryManager.UserAuthentication.GetEmailAsync(user.UserName))!,
                "StudyControlSoftware - Confirmation Email",
                _repositoryManager.Assets.GetEmailConfirmationMessage(
                    confirmationLink));

            return _user == null
                ? BadRequest()
                : Ok(_user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserRegisterDto user)
        {
            var _user = await _repositoryManager.Admins.UpdateAsync(id, user);

            return _user == null
                ? BadRequest()
                : Ok(_user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var removedUserId = await _repositoryManager.Admins.DeleteAsync(id);

            return removedUserId == null
               ? BadRequest()
               : Ok(removedUserId);
        }
    }
}

