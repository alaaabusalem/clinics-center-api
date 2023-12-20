using clinics_api.Models.DTOs;

namespace clinics_api.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsDisabal { get; set; }

        // nav props

        public List<Doctor> Doctors { get; set; }

        public static explicit operator Department(CreatDepartmentDto dep)
        {
            return new Department
            {
                DepartmentName = dep.DepartmentName,
                IsDisabal = false,


            };
        }
        public static explicit operator Department(UpdateDepartmentDto dep)
        {
            return new Department
            {
                DepartmentName = dep.DepartmentName,
                IsDisabal = dep.IsDisabal,

            };
        }

        
    }
}
