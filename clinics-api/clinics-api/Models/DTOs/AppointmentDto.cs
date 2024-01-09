namespace clinics_api.Models.DTOs
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string PatientAge { get; set; }
        public string ContactNumber { get; set; }
        public string Date { get; set; }
        public string time { get; set; }
        public int DoctorId { get; set; }
        public AppointmentStatusDto appointmentStatus { get; set; }
        //public DoctorDto doctor { get; set; }
        public string DoctorName { get; set; }

    }
    public class DoctorAppointmentDto
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string PatientAge { get; set; }
        public string ContactNumber { get; set; }
        public string Date { get; set; }
        public string time { get; set; }
        public int DoctorId { get; set; }
        public AppointmentStatusDto appointmentStatus { get; set; }
        public UserDto user { get; set; }

    }
    public class CreatAppointmentDto
    {
        public string PatientName { get; set; }
        public string PatientAge { get; set; }
        public string ContactNumber { get; set; }
        public string Date { get; set; }
        public string time { get; set; }
        public int DoctorId { get; set; }
    }

   
}
