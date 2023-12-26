using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clinics_api.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/user")]

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userDb;
        public UserController(IUser user)
        {
            _userDb = user;
        }

        [Route("RegisterUser")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegesterUserDto regesterUserDto)
        {
            if(!this.ModelState.IsValid) { return BadRequest(); }
          var result=  await _userDb.RegesterUserOrManager(regesterUserDto, this.ModelState, "Patient");
            if (!this.ModelState.IsValid) { return BadRequest(new ValidationProblemDetails(ModelState)); }
            return Ok();
        }
        [Route("RegisterAdmin")]

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegesterUserDto regesterUserDto)
        {
            if (!this.ModelState.IsValid) { return BadRequest(); }
            var result = await _userDb.RegesterUserOrManager(regesterUserDto, this.ModelState, "Admin");
            if (!this.ModelState.IsValid) { return BadRequest(new ValidationProblemDetails(ModelState)); }
            return Ok();
        }

        [Route("RegisterDoctor")]

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterDoctor([FromForm] RegesterDoctorUserDto regesterDoctorUserDto)
        {
           if (!this.ModelState.IsValid) { return BadRequest(); }
            var result = await _userDb.RegesterDoctor(regesterDoctorUserDto, this.ModelState);
            if (!this.ModelState.IsValid) { return BadRequest(new ValidationProblemDetails(ModelState)); }
            return Ok();
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!this.ModelState.IsValid) { return BadRequest(); }
            var result = await _userDb.Login(loginDto);
            if (result==null) { return BadRequest("Wrong Email Or Password"); }
            return Ok(result);
        }
    }
}
