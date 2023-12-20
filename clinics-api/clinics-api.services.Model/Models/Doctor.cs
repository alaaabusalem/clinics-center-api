
using clinics_api.services.Model.DTOs;

namespace clinics_api.services.Model.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Img { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string LocationDetailes { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public string Description { get; set; }
        public string fees { get; set; }

        public bool IsDisabal { get; set; }

        // foreign keys
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public string UserId { get; set; }


        // nav props
        public Department? department { get; set; }
        public Location? location { get; set; }

        public ApplicationUser? ApplicationUser { get; }


        public static explicit operator Doctor(RegesterDoctorUserDto app)
        {
            return new Doctor
            {
                Name = app.name,
                Phone = app.PhoneNumber,
                Gender=app.Gender,
                Img=app.Img,
                Specialization = app.Specialization,
                LocationDetailes=app.LocationDetailes,
                OpeningTime=app.OpeningTime,    
                CloseTime=app.CloseTime,
                Description=app.Description,
                fees=app.fees,
                DepartmentId=app.DepartmentId,
                LocationId=app.LocationId,
                IsDisabal=false
            };
        }
    }

}

