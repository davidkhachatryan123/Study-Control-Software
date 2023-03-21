using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Education;
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
        public async Task<IActionResult> Create([FromBody] FacultyDto faculty)
        {
            return Ok(await _repositoryManager.Faculties.CreateAsync(faculty));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] FacultyDto faculty)
        {
            FacultyDto? updatedFaculty = await _repositoryManager.Faculties.UpdateAsync(id, faculty);

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


        [HttpPost("{id}/GetCourses")]
        public async Task<IActionResult> GetCourses(int id)
        {
            return Ok(await _repositoryManager.Faculties.GetAllCoursesAsync(id));
        }

        [HttpPost("{id}/AddCourse")]
        public async Task<IActionResult> AddCourse(int id, [FromBody] List<int> courses)
        {
            await _repositoryManager.Faculties.AddCoursesAsync(id, courses);

            return Ok();
        }

        [HttpDelete("{id}/DeleteCourse/{courseId}")]
        public async Task<IActionResult> DeleteCourse(int id, int courseId)
        {
            bool result = await _repositoryManager.Faculties.DeleteCourseAsync(id, courseId);

            return !result
                ? BadRequest()
                : Ok();
        }
    }
}

