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
    public class CoursesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public CoursesController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TableOptionsDto options)
        {
            return Ok(await _repositoryManager.Courses.GetAllAsync(options));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDto course)
        {
            return Ok(await _repositoryManager.Courses.CreateAsync(course));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] CourseDto course)
        {
            CourseDto? updatedCourse = await _repositoryManager.Courses.UpdateAsync(id, course);

            return updatedCourse == null
                ? BadRequest()
                : Ok(updatedCourse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int? deleteId = await _repositoryManager.Courses.DeleteAsync(id);

            return deleteId == null
                ? BadRequest()
                : Ok(deleteId);
        }


        [HttpGet("{id}/GetLecturer")]
        public async Task<IActionResult> GetLecturer(int id)
        {
            var lecturer = await _repositoryManager.Courses.GetLecturerAsync(id);

            return lecturer == null
                ? BadRequest()
                : Ok(lecturer);
        }

        [HttpPost("{id}/SetLecturer")]
        public async Task<IActionResult> SetLecturer(int id, [FromBody] string lecturerId)
        {
            var lecturer = await _repositoryManager.Courses.SetLecturerAsync(id, lecturerId);

            return lecturer == null
                ? BadRequest()
                : Ok(lecturer);
        }

        [HttpDelete("{id}/DeleteLecturer")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await _repositoryManager.Courses.DeleteLecturerAsync(id);

            return lecturer == null
                ? BadRequest()
                : Ok(lecturer);
        }
    }
}

