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
    public class ManageLecturersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public ManageLecturersController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TableOptionsDto options)
        {
            return Ok(await _repositoryManager.Lecturers.GetAllAsync(options));
        }

        [HttpPost]
        [ServiceFilter(typeof(EnsureUserNoExistsFilter))]
        public async Task<IActionResult> Create([FromBody] UserRegisterDto user)
        {
            UserDto? newUser = await _repositoryManager.Lecturers.CreateLecturer(user);

            return newUser == null
                ? StatusCode(StatusCodes.Status500InternalServerError)
                : Ok(newUser);
        }
    }
}
