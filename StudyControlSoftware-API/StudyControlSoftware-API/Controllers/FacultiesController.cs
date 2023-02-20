using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.Controllers
{
    [Authorize(Policy = nameof(UserRoles.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public FacultiesController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TableOptionsDto options)
        {
            return Ok(await _repositoryManager.Faculties.GetAllAsync(options));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Faculty faculty)
        {
            return Ok(await _repositoryManager.Faculties.CreateAsync(faculty));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Faculty faculty)
        {
            Faculty? updatedFaculty = await _repositoryManager.Faculties.UpdateAsync(id, faculty);

            return updatedFaculty == null
                ? BadRequest()
                : Ok(updatedFaculty);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int? deleteId = await _repositoryManager.Faculties.DeleteAsync(id);

            return deleteId == null
                ? BadRequest()
                : Ok(deleteId);
        }
    }
}

