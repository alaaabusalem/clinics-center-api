
using clinics_api.services.Model.DTOs;
using clinics_api.services.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clinics_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userDb;
        public UserController(IUser user)
        {
            _userDb = user;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegesterUserDto regesterUserDto)
        {
            if(!this.ModelState.IsValid) { return BadRequest(); }
          var result=  await _userDb.RegesterUserOrManager(regesterUserDto, this.ModelState, "Patient");
            if (!this.ModelState.IsValid) { return BadRequest(new ValidationProblemDetails(ModelState)); }
            return Ok(" Regester new Patient user done succesfully");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegesterUserDto regesterUserDto)
        {
            if (!this.ModelState.IsValid) { return BadRequest(); }
            var result = await _userDb.RegesterUserOrManager(regesterUserDto, this.ModelState, "Admin");
            if (!this.ModelState.IsValid) { return BadRequest(new ValidationProblemDetails(ModelState)); }
            return Ok(" Regester new Admin user done succesfully");
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegesterDoctorUserDto regesterDoctorUserDto)
        {
            if (!this.ModelState.IsValid) { return BadRequest(); }
            var result = await _userDb.RegesterDoctor(regesterDoctorUserDto, this.ModelState);
            if (!this.ModelState.IsValid) { return BadRequest(new ValidationProblemDetails(ModelState)); }
            return Ok(" Regester new Doctor user done succesfully");
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!this.ModelState.IsValid) { return BadRequest(); }
            var result = await _userDb.Login(loginDto);
            if (!result.isAuth) { return BadRequest(result); }
            return Ok(result);
        }
    }
}
