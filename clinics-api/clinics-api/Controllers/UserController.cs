using clinics_api.Models;
using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public async Task<ActionResult<int>> RegisterDoctor(string postDtoJson,IFormFile img)
        {
           if (!this.ModelState.IsValid) { return BadRequest(); }
            var postDto = JsonConvert.DeserializeObject<RegesterDoctorUserDto>(postDtoJson);

            var docId = await _userDb.RegesterDoctor(postDto, this.ModelState);
            if (!this.ModelState.IsValid) { return BadRequest(new ValidationProblemDetails(ModelState)); }
            if(docId == -1) return BadRequest();
            if (img == null || img.Length == 0)
            {
                return BadRequest("The ImageFile field is required.");
            }
            var result = await _userDb.RegesterDoctorImg(docId, img);
            if (result) return docId;

            return BadRequest();
        }
        ///{doctorId
        [Route("RegisterDoctorImg")]

        [HttpPost]
        public async Task<IActionResult> updateProfilePicture([FromBody] IFormFile img)
        {
            if (img == null || img.Length == 0)
            {
                return BadRequest("The ImageFile field is required.");
            }
           // var result = await _userDb.RegesterDoctorImg(doctorId, ImageFile);
           // if (result) return Ok();

            return BadRequest();

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
