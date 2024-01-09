using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace clinics_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointment _Db;
        public AppointmentsController(IAppointment db)
        {
            _Db = db;
        }


        [Route("creat")]

        [HttpPost]
        [Authorize(Roles = "Patient")]

        public async Task<ActionResult<bool>> CreatAppointment(CreatAppointmentDto creatAppointmentDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID

            var result= await _Db.CreateAppointment(creatAppointmentDto, userId);
            if (result) return Ok();
            return BadRequest();
        }

        [Route("Client/appointments")]

        [HttpGet]
        [Authorize(Roles = "Patient")]

        public async Task<ActionResult<List<AppointmentDto>>> GetClientAppointments( )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID

            var result = await _Db.UserAppointments(userId);
            return result;
        }
        [Route("Doctor/appointments")]

        [HttpGet]
        [Authorize(Roles = "Doctor")]

        public async Task<ActionResult<List<DoctorAppointmentDto>>> GetDoctorAppointments( )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID

            var result = await _Db.DoctorAppointments(userId);
            return result;
        }
    }
}
