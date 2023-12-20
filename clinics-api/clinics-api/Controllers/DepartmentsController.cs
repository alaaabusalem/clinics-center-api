
using clinics_api.services.Model.DTOs;
using clinics_api.services.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clinics_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartment _Db;
        public DepartmentsController(IDepartment db)
        {
            _Db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments() { 
           return Ok(await _Db.GetDepartments());   
                 
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentDoctors(int id)
        {
            var dep = await _Db.GetDoctorDepartment(id);
            if (dep == null) return BadRequest();
            return Ok(dep);

        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreatDepartmentDto creatDepartmentDto)
        {
            if(!this.ModelState.IsValid) { return BadRequest(); }
            var dep = await _Db.CreatDepartment(creatDepartmentDto);
            if (dep == false) return BadRequest();
            return Ok("Department has been created");

        }
        [Authorize(Roles = "Admin")]

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody]UpdateDepartmentDto updateDepartmentDto,int id)
        {
            if (!this.ModelState.IsValid) { return BadRequest(); }
            var dep = await _Db.UpdateDepartment(updateDepartmentDto,id);
            if (dep == false) return BadRequest();
            return Ok("Department has been Updated");

        }
        [Authorize(Roles = "Admin")]

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dep = await _Db.DeleteDepartment(id);
            if (dep == false) return BadRequest();
            return Ok("Department has been deleted");

        }
    }
}
