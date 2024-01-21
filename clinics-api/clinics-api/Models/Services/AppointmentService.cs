using clinics_api.Data;
using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

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

        public async Task<List<AppointmentStatusDto>> AppointmentStatusList()
        {
            var appointmentStatusList = await _Db.AppointmentStatuses
                .Select(appStatus => new AppointmentStatusDto
                {
                    AppointmentStatusId = appStatus.AppointmentStatusId,
                    name = appStatus.name
                })
                .ToListAsync();
            return appointmentStatusList;
        }

        public async Task<bool> CreateAppointment(CreatAppointmentDto creatAppointmentDto, string userId)
        {
            var user= await _userManager.FindByIdAsync(userId);
            var doctor = await _Db.Doctors.FirstOrDefaultAsync(doc=> doc.DoctorId== creatAppointmentDto.DoctorId);
            if(doctor!=null && user != null)
            {
                var appoint = await _Db.Appointments
             .FirstOrDefaultAsync(app => app.DoctorId == doctor.DoctorId && app.Date == DateTime.Parse(creatAppointmentDto.Date)&& app.time== TimeSpan.Parse(creatAppointmentDto.time));
                if (appoint != null) return false;
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
                  
                       userId= app.ApplicationUser.Id,
                       userName=app.ApplicationUser.UserName,
                  
                    })
                    .OrderByDescending(app => app.Date)
                    .ThenByDescending(app => app.time)
                    .ToListAsync();
                return DoctorAppointments;
            }
            return null;
        }

        public async Task<List<DoctorAppointmentDto>> DoctorAppointmentsByStatus(string userId, int appointmentStatusId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var doctor = await _Db.Doctors.FirstOrDefaultAsync(doc => doc.UserId == userId);
                var DoctorAppointments = await _Db.Appointments
                  .Where(app => app.DoctorId == doctor.DoctorId && app.AppointmentStatusId==appointmentStatusId)
                    .Select(app => new DoctorAppointmentDto
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

                        userId = app.ApplicationUser.Id,
                        userName = app.ApplicationUser.UserName,

                    })
                    .OrderByDescending(app => app.Date)
                    .ThenByDescending(app => app.time)
                    .ToListAsync();
                return DoctorAppointments;
            }
            return null;
        }

        public async Task<UpdateAppointmentDto> GetDoctorAppointment(string userId, int appointmentIdd)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var doctor = await _Db.Doctors.FirstOrDefaultAsync(doc => doc.UserId == userId);
                var DoctorAppointment = await _Db.Appointments
                    .Where(ap => ap.DoctorId == doctor.DoctorId && ap.AppointmentId == appointmentIdd)
                    .Select(app => new UpdateAppointmentDto
                    {
                        AppointmentId = app.AppointmentId,
                        PatientName = app.PatientName,
                        ContactNumber = app.ContactNumber,
                        PatientAge = app.PatientAge,
                        Description = app.Description,
                        Medicines = app.Medicines,
                        Date = app.Date.ToString(),
                        time = app.time.ToString(),
                        AppointmentStatusId = app.AppointmentStatus.AppointmentStatusId
                    }).FirstAsync();


                return DoctorAppointment;
            }
            return null;
        }

        public async Task<bool> UpdateAppointment(string userId, UpdateAppointmentDto updateAppointmentDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var doctor = await _Db.Doctors.FirstOrDefaultAsync(doc => doc.UserId == userId);
                var appointment = await _Db.Appointments
                  .FirstOrDefaultAsync(app => app.DoctorId == doctor.DoctorId&& app.AppointmentId==updateAppointmentDto.AppointmentId);
                if (appointment != null)
                {
                    appointment.PatientName=updateAppointmentDto.PatientName;
                    appointment.PatientAge = updateAppointmentDto.PatientAge;
                    appointment.AppointmentStatusId = updateAppointmentDto.AppointmentStatusId;
                    appointment.ContactNumber = updateAppointmentDto.ContactNumber;
                    appointment.Description = updateAppointmentDto.Description;
                    appointment.Medicines = updateAppointmentDto.Medicines;
                    _Db.Entry(appointment).State = EntityState.Modified;
                    await _Db.SaveChangesAsync();
                    return true;
                }      
            
            }
            return false;
        }

        public async Task<AppointmentDto> UserAppointment(string userId, int appointmentIdd)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var appointment = await _Db.Appointments
                    .Where(appoin => appoin.UserId == user.Id&& appoin.AppointmentId== appointmentIdd)
                    .Select(app => new AppointmentDto
                    {
                        AppointmentId = app.AppointmentId,
                        PatientName = app.PatientName,
                        ContactNumber = app.ContactNumber,
                        PatientAge = app.PatientAge,
                        Date = app.Date.ToString(),
                        time = app.time.ToString(),
                        DoctorId = app.DoctorId,
                        Description = app.Description,
                        Medicines = app.Medicines,
                        appointmentStatus = new AppointmentStatusDto()
                        {
                            AppointmentStatusId = app.AppointmentStatus.AppointmentStatusId,
                            name = app.AppointmentStatus.name,
                        },

                        DoctorName = app.Doctor.Name,
                        DoctorPhone = app.Doctor.Phone


                    })
                   .FirstAsync();
                return appointment;
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
                        Description=app.Description,
                        Medicines=app.Medicines,
                        appointmentStatus = new AppointmentStatusDto()
                        {
                            AppointmentStatusId = app.AppointmentStatus.AppointmentStatusId,
                            name = app.AppointmentStatus.name,
                        },

                        DoctorName = app.Doctor.Name,
                        DoctorPhone=app.Doctor.Phone


                    })
                    .OrderByDescending(app => app.Date)
                    .ThenByDescending(app => app.time)
                   .ToListAsync(); 
                return appointments;
            }
            return null;
        }

        public async Task<List<AppointmentDto>> UserAppointmentsByStatus(string userId, int appointmentStatusId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //var doctor = await _Db.Doctors.FirstOrDefaultAsync(doc => doc.UserId == userId);
                var appointments = await _Db.Appointments
                    .Where(appoin => appoin.UserId == user.Id && appoin.AppointmentStatusId==appointmentStatusId)
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

                        DoctorName = app.Doctor.Name,


                    })
                    .OrderByDescending(app => app.Date)
                    .ThenByDescending(app => app.time)
                   .ToListAsync();
                return appointments;
            }
            return null;
        }
    }
}
