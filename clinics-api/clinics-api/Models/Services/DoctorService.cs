using clinics_api.Data;
using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System;

namespace clinics_api.Models.Services
{
    public class DoctorService : IDoctor
    {
        private readonly ClinicsDbContext _context;
        public DoctorService(ClinicsDbContext context)
        {
            _context = context;
        }

        public async Task<List<DateSlot>> GetDateSlots(int doctorId)
        {
            var dateSlots = new List<DateSlot>();

            var doctor = await _context.Doctors.FirstOrDefaultAsync(doc => doc.DoctorId == doctorId && doc.IsDisabal == false);
            if (doctor != null)
            {
                string jordanTimeZoneId = "Jordan Standard Time";
                TimeZoneInfo jordanTimeZone = TimeZoneInfo.FindSystemTimeZoneById(jordanTimeZoneId);

                // Get the current UTC time
                DateTime utcNow = DateTime.UtcNow;

                // Convert UTC time to local time in Jordan
                var today = TimeZoneInfo.ConvertTimeFromUtc(utcNow, jordanTimeZone);

                var endTime = doctor.CloseTime;
                var Day = today;
                while (dateSlots.Count < 9)
                {
                    var startTime = doctor.OpeningTime;
                    if (Day.DayOfWeek != DayOfWeek.Friday)
                    {
                        if (Day.Date == DateTime.Now.Date && DateTime.Now.TimeOfDay > startTime.Add(new TimeSpan(0, 15, 0)))
                        {
                            startTime =SharedMethods.RoundToNearestQuarterHour(DateTime.Now.TimeOfDay);
                        }

                        DateSlot dateSlot = new DateSlot()
                        {
                            date = Day.ToString(),
                            timeSlots = new List<TimeSlot>()
                        };

                        for (TimeSpan i = startTime; i <= endTime; i = i.Add(new TimeSpan(0, 15, 0)))
                        {
                            bool isAvaliable = true;

                            var timeSpan = await _context.Appointments.
                                FirstOrDefaultAsync(app => app.DoctorId == doctorId &&
                                app.Date.Date == Day.Date
                                && app.time == i);
                            if (timeSpan != null) { isAvaliable = false; }
                            dateSlot.timeSlots.Add(new TimeSlot()
                            {
                                time = i.ToString(),
                                isAvailable = isAvaliable,
                            });
                        }
                        dateSlots.Add(dateSlot);
                    }

                    Day = Day.Date.AddDays(1);

                }
            }
            return dateSlots;

        }

        public async Task<List<DoctorBookDto>> GetDocByLocation(int locationId)
        {
            var docs = await _context.Doctors.Where(doc => doc.LocationId == locationId)
                .Select(doctor => new DoctorBookDto
                {
                    DoctorId = doctor.DoctorId,
                    Name = doctor.Name,
                    Phone = doctor.Phone,
                    LocationDetailes = doctor.LocationDetailes,
                    IsDisabal = doctor.IsDisabal,
                    Img = doctor.Img,
                    Gender = doctor.Gender,
                    Specialization = doctor.Specialization,
                    OpeningTime = doctor.OpeningTime.ToString(),
                    CloseTime = doctor.CloseTime.ToString(),
                    Description = doctor.Description,
                    fees = doctor.fees,
                 LocationName= doctor.location.LocationName,
                    
                 DepartmentName = doctor.department.DepartmentName,
                    
                })
             .ToListAsync();
            return docs;
        }

       

       

        public async Task<List<DoctorBookDto>> GetDocBySpecialization(int departmentId)
        {
            var docs = await _context.Doctors.Where(doc => doc.DepartmentId == departmentId)
                .Select(doctor => new DoctorBookDto
                {
                    DoctorId = doctor.DoctorId,
                    Name = doctor.Name,
                    Phone = doctor.Phone,
                    LocationDetailes = doctor.LocationDetailes,
                    IsDisabal = doctor.IsDisabal,
                    Img = doctor.Img,
                    Gender = doctor.Gender,
                    Specialization = doctor.Specialization,
                    OpeningTime = doctor.OpeningTime.ToString(),
                    CloseTime = doctor.CloseTime.ToString(),
                    Description = doctor.Description,
                    fees = doctor.fees,
                    LocationName = doctor.location.LocationName,

                    DepartmentName = doctor.department.DepartmentName,
                })
             .ToListAsync();
            return docs;
        }

        public async Task<List<DoctorBookDto>> GetDocByLocationAndSpecialization(int locationId, int departmentId)
        {
            var docs = await _context.Doctors.Where(doc => doc.DepartmentId == departmentId && doc.LocationId==locationId)
                .Select(doctor => new DoctorBookDto
                {
                    DoctorId = doctor.DoctorId,
                    Name = doctor.Name,
                    Phone = doctor.Phone,
                    LocationDetailes = doctor.LocationDetailes,
                    IsDisabal = doctor.IsDisabal,
                    Img = doctor.Img,
                    Gender = doctor.Gender,
                    Specialization = doctor.Specialization,
                    OpeningTime = doctor.OpeningTime.ToString(),
                    CloseTime = doctor.CloseTime.ToString(),
                    Description = doctor.Description,
                    fees = doctor.fees,
                    LocationName = doctor.location.LocationName,

                    DepartmentName = doctor.department.DepartmentName,
                })
             .ToListAsync();
            return docs;
        }


        public async Task<DoctorBookDto> GetDocDateSlots(int doctorId)
        {
            var docBook = await _context.Doctors
                .Select(doctor => new DoctorBookDto
                {
                    DoctorId = doctor.DoctorId,
                    Name = doctor.Name,
                    Phone = doctor.Phone,
                    LocationDetailes = doctor.LocationDetailes,
                    IsDisabal = doctor.IsDisabal,
                    Img = doctor.Img,
                    Gender = doctor.Gender,
                    Specialization = doctor.Specialization,
                    OpeningTime = doctor.OpeningTime.ToString(),
                    CloseTime = doctor.CloseTime.ToString(),
                    Description = doctor.Description,
                    fees = doctor.fees,
                    LocationName = doctor.location.LocationName,

                    DepartmentName = doctor.department.DepartmentName,
                })

                .FirstOrDefaultAsync(doc => doc.DoctorId == doctorId && doc.IsDisabal == false);
            var slots = await GetDateSlots(doctorId);
            docBook.dateSlots = new List<DateSlot>();
            docBook.dateSlots.AddRange(slots);
            return docBook;
        }

       
    }
}

