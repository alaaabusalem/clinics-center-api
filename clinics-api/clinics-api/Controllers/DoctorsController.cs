using clinics_api.Models;
using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace clinics_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorsController : ControllerBase
    {

        private readonly IDoctor _Db;
        public DoctorsController(IDoctor db)
        {
            _Db = db;
        }

        [Route("Doctor/{doctorId}")]
        [HttpGet]

        public async Task< ActionResult<DoctorBookDto>> GetDoctorDateSlots(int doctorId)
        {
            return await _Db.GetDocDateSlots(doctorId);
        }
        [Route("DateSlots")]
        [HttpGet]

        public async Task<ActionResult<List<DateSlot>>> GeDateSlots(int doctorId)
        {
           var dates=  await _Db.GetDateSlots(doctorId);
            return dates;   
        }
        [Route("doctors/location/{locationId}")]
        [HttpGet]

        public async Task<ActionResult<List<DoctorBookDto>>> GeDoctorsByLocation(int locationId)
        {
            var docs = await _Db.GetDocByLocation(locationId);
            return docs;
        }

        [Route("doctors/department/{departmentId}")]
        [HttpGet]

        public async Task<ActionResult<List<DoctorBookDto>>> GeDoctorsByDepartment(int departmentId)
        {
            var docs = await _Db.GetDocBySpecialization(departmentId);
            return docs;
        }

        [Route("doctors/location/{locationId}/department/{departmentId}")]
        [HttpGet]

        public async Task<ActionResult<List<DoctorBookDto>>> GeDoctorsByDepartmentAndLocation(int locationId, int departmentId)
        {
            var docs = await _Db.GetDocByLocationAndSpecialization(locationId,departmentId);
            return docs;
        }
    }
}
