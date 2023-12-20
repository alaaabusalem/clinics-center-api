namespace clinics_api.services.Model.Models
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
