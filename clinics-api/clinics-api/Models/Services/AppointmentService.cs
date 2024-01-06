using clinics_api.Data;
using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace clinics_api.Models.Services
{
    public class AppointmentService : IAppointment
    {
        private readonly ClinicsDbContext _Db;
        private readonly UserManager<ApplicationUser> _userManager;
        public AppointmentService(ClinicsDbContext db,UserManager<ApplicationUser> userManager)
        {
            _Db = db;
            _userManager = userManager;
        }
        public async Task<bool> CreateAppointment(CreatAppointmentDto creatAppointmentDto, string userId)
        {
            var user= await _userManager.FindByIdAsync(userId);
            var doctor = await _Db.Doctors.FirstOrDefaultAsync(doc=> doc.DoctorId== creatAppointmentDto.DoctorId);
            if(doctor!=null && user != null)
            {
                var appointment = (Appointment)creatAppointmentDto;
                appointment.AppointmentStatusId = 1;
                appointment.UserId=userId;
                appointment.Description = "";
                appointment.Medicines = "";
                var app= await _Db.Appointments.AddAsync(appointment);
                await _Db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<DoctorAppointmentDto>> DoctorAppointments(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var doctor= await _Db.Doctors.FirstOrDefaultAsync(doc=> doc.UserId== userId);
                var DoctorAppointments = await _Db.Appointments
                  .Where(app => app.DoctorId == doctor.DoctorId)
                    .Select(app=> new DoctorAppointmentDto
                    {
                      AppointmentId= app.AppointmentId,
                      PatientName=app.PatientName,
                      ContactNumber=app.ContactNumber,
                        PatientAge=app.PatientAge,
                      Date= app.Date.ToString(),
                      time= app.time.ToString(),
                      DoctorId=app.DoctorId,
                   appointmentStatus =new AppointmentStatusDto()
                   {
                       AppointmentStatusId=app.AppointmentStatus.AppointmentStatusId,
                       name= app.AppointmentStatus.name,
                   },
                   user= new UserDto()
                   {
                       userId= app.ApplicationUser.Id,
                       name=app.ApplicationUser.UserName,
                       PhoneNumber=app.ApplicationUser.PhoneNumber,
                       Email=app.ApplicationUser.Email
                   }
                    })
                    .ToListAsync();
                return DoctorAppointments;
            }
            return null;
        }

        public async Task<List<AppointmentDto>> UserAppointments(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //var doctor = await _Db.Doctors.FirstOrDefaultAsync(doc => doc.UserId == userId);
                var appointments = await _Db.Appointments
                    .Where(appoin => appoin.UserId == user.Id)
                    .Select(app => new AppointmentDto
                    {
                        AppointmentId = app.AppointmentId,
                        PatientName = app.PatientName,
                        ContactNumber = app.ContactNumber,
                        PatientAge = app.PatientAge,
                        Date = app.Date.ToString(),
                        time = app.time.ToString(),
                        DoctorId = app.DoctorId,
                        appointmentStatus = new AppointmentStatusDto()
                        {
                            AppointmentStatusId = app.AppointmentStatus.AppointmentStatusId,
                            name = app.AppointmentStatus.name,
                        },
                        doctor= new DoctorDto()
                        {
                            Name= app.Doctor.Name,
                            fees=app.Doctor.fees,
                            LocationName=app.Doctor.location.LocationName,
                            LocationDetailes=app.Doctor.LocationDetailes,
                            DepartmentName=app.Doctor.department.DepartmentName,
                            Phone=app.Doctor.Phone
                        }
                    })
                    
                    .ToListAsync();
                return appointments;
            }
            return null;
        }
    }
}
