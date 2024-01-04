using System.Runtime.Serialization;

namespace clinics_api.Models.DTOs
{
    public class DoctorBookDto
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Img { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string LocationDetailes { get; set; }
        public string OpeningTime { get; set; }
        public string CloseTime { get; set; }
        public string Description { get; set; }
        public string fees { get; set; }

        public bool IsDisabal { get; set; }

        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
        public List<DateSlot> dateSlots { get; set;}
        
    }



    public class DateSlot
    {

        public string date { get; set; }


        public List<TimeSlot> timeSlots { get; set; }
    }
    public class TimeSlot
    {

        public string time { get; set; }


        public bool isAvailable { get; set; }
    }
}
