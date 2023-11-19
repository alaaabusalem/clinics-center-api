namespace clinics_api.Models
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
    }

}

