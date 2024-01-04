namespace clinics_api.Models.DTOs
{
    public class LocationDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public bool IsDisabal { get; set; }
    }

    public class LocationDoc
    {
        public string LocationName { get; set; }
        
    }
}
