namespace clinics_api.Models
{
    public class AppointmentStatus
    {
        public int AppointmentStatusId { get; set; }
        public string name { get; set; }
        public bool IsDisabal { get; set; }

        // nav props

        public List<Appointment> appointments { get; set; }

    }
}
