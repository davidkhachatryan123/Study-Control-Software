using System;
using Microsoft.AspNetCore.Mvc;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.Controllers
{
    [Route("api/[controller]")]
    public class ManageAdminsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public ManageAdminsController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<UsersTableDto> Get([FromQuery] TableOptionsDto options)
        {
            return await _repositoryManager.Admins.GetAdminsAsync(options);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {

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

