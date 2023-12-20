namespace clinics_api.services.Model.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string PatientAge { get; set; }
        public string ContactNumber { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan time { get; set; }
        public string Description { get; set; }
        public string Medicines { get; set; }
        // foreign keys
        public int DoctorId { get; set; }
        public string? UserId { get; set; }
        public int AppointmentStatusId { get; set; }

        // nav props
        public Doctor? Doctor { get; set; }
        public ApplicationUser? ApplicationUser { get; }
        public AppointmentStatus ?AppointmentStatus { get; set; }

    }
}
