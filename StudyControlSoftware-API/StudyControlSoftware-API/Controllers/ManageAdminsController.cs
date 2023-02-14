using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
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
            return Ok(await _repositoryManager.Admins.GetAdminsAsync(options));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegisterDto user)
        {
            var _user = await _repositoryManager.Admins.RegisterNewAdmin(user);

            // TODO: Send email for Confirm Email!

            return _user == null
                ? BadRequest()
                : Ok(_user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}

