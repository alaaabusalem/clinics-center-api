using clinics_api.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinics_api.Models
{
    public class ApplicationUser:IdentityUser
    {
        public Doctor? doctor { get; set; }
        public List<Appointment> appointments { get; set; }





        public static explicit operator ApplicationUser(RegesterUserDto app)
        {
            return new ApplicationUser
            {
                UserName=app.name,
                Email=app.Email,
                PhoneNumber=app.PhoneNumber,    

            };
        }
    }
}
