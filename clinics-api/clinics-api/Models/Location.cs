namespace clinics_api.Models
{
    public class Location
    {
        public int LocationId { get; set; } 
        public string LocationName { get; set; }
        public bool IsDisabal { get; set; }

        // nav props
        public List<Doctor> doctors { get; }

    }
}
