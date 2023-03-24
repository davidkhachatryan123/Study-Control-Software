using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Database.Models;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UserRegisterDto user)
        {
            var _user = await _repositoryManager.Students.UpdateAsync(id, user);

            return _user == null
                ? BadRequest()
                : Ok(_user);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var removedUserId = await _repositoryManager.Students.RemoveAsync(id);

            return removedUserId == null
               ? BadRequest()
               : Ok(new { removed = removedUserId });
        }


        [HttpGet("{id}/GetFaculty")]
        public async Task<IActionResult> GetFaculty(string id)
        {
            return  Ok(await _repositoryManager.Students.GetFacultyAsync(id));
        }

        [HttpPost("{id}/SetFaculty")]
        public async Task<IActionResult> SetFaculty(string id, int facultyId)
        {
            var faculty = await _repositoryManager.Students.SetFacultyAsync(id, facultyId);

            return faculty == null
                ? BadRequest()
                : Ok(faculty);
        }

        [HttpDelete("{id}/DeleteFaculty")]
        public async Task<IActionResult> DeleteFaculty(string id)
        {
            var faculty = await _repositoryManager.Students.DeleteFacultyAsync(id);

            return faculty == null
                ? BadRequest()
                : Ok(faculty);
        }
    }
}

