using clinics_api.services.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace clinics_api.services.Model.DTOs
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsDisabal { get; set; }

    }

    public class CreatDepartmentDto
    {

        [Required]

        public string DepartmentName { get; set; }
    }

    public class UpdateDepartmentDto
    {
        [Required]

        public string DepartmentName { get; set; }
        [Required]

        public bool IsDisabal { get; set; }
    }
    public class DepartmentDoctorsDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsDisabal { get; set; }
        public List<Doctor> Doctors { get; set; }

    }
}
