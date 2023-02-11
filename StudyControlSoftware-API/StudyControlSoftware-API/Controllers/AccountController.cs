using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Filters;
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

        [Route("Setup")]
        [HttpGet]
        [SetupResourceFilter]
        public async Task<IActionResult> Get()
        {
            var result = await _repositoryManager.UserAuthentication.Setup();

            return !result.Succeeded ? new BadRequestObjectResult(result) : StatusCode(201);
        }
    }
}
