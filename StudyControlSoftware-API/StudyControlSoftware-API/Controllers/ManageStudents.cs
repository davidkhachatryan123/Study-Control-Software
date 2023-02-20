using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Filters;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.Controllers
{
    [Authorize(Policy = nameof(UserRoles.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class ManageStudentsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public ManageStudentsController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TableOptionsDto options)
        {
            return Ok(await _repositoryManager.Students.GetAllAsync(options));
        }

        [HttpPost]
        [EnsureUserExistsFilter(No = true)]
        public async Task<IActionResult> Create([FromBody] UserRegisterDto user)
        {
            UserDto? newUser = await _repositoryManager.Students.CreateAsync(user);

            return newUser == null
                ? StatusCode(StatusCodes.Status500InternalServerError)
                : Ok(newUser);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] UserRegisterDto user)
        {
            var _user = await _repositoryManager.Students.UpdateAsync(id, user);

            return _user == null
                ? BadRequest()
                : Ok(_user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var removedUserId = await _repositoryManager.Students.RemoveAsync(id);

            return removedUserId == null
               ? BadRequest()
               : Ok(removedUserId);
        }
    }
}

