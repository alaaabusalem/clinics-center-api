namespace clinics_api.Models
{
    public class Department
    {
        public int DepartmentId { get; set; } 
        public string DepartmentName { get; set; }
        public bool IsDisabal { get; set; }

        // nav props

        public List<Doctor> Doctors { get; set; }


    }
}
