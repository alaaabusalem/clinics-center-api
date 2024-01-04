using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clinics_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartment _Db;
        public DepartmentsController(IDepartment db)
        {
            _Db = db;
        }

        [Route("GetDepartments")]

        [HttpGet]

        public async Task<ActionResult<List<DepartmentDto>>> GetDepartments() { 
           return await _Db.GetDepartments();   
                 
        }

        [Route("GetLocations")]

        [HttpGet]

        public async Task<ActionResult<List<LocationDto>>> GetLocations()
        {
            return await _Db.GetLocations();

        }

        [Route("GetDepartmentDoctors")]

        [HttpGet]
        public async Task<IActionResult> GetDepartmentDoctors(int id)
        {
            var dep = await _Db.GetDoctorDepartment(id);
            if (dep == null) return BadRequest();
            return Ok(dep);

        }

        [Route("CreateDepartment")]
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreatDepartmentDto creatDepartmentDto)
        {
            if(!this.ModelState.IsValid) { return BadRequest(); }
            var dep = await _Db.CreatDepartment(creatDepartmentDto);
            if (dep == false) return BadRequest();
            return Ok();

        }

        [Route("UpdateDepartment")]

        [Authorize(Roles = "Admin")]

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody]UpdateDepartmentDto updateDepartmentDto,int id)
        {
            if (!this.ModelState.IsValid) { return BadRequest(); }
            var dep = await _Db.UpdateDepartment(updateDepartmentDto,id);
            if (dep == false) return BadRequest();
            return Ok();

        }

        [Route("UpdateDepartment")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dep = await _Db.DeleteDepartment(id);
            if (dep == false) return BadRequest();
            return Ok();

        }
    }
}
