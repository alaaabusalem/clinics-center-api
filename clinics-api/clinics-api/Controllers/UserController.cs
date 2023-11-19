using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
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
        [Authorize]
        public async Task<IActionResult> RegisterUser([FromBody] RegesterUserDto regesterUserDto)
        {
            if(!this.ModelState.IsValid) { return BadRequest(); }
          var result=  await _userDb.RegesterUser(regesterUserDto, this.ModelState);
            if (!this.ModelState.IsValid) { return BadRequest(new ValidationProblemDetails(ModelState)); }
            return Ok(" Regester new user done succesfully");
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
