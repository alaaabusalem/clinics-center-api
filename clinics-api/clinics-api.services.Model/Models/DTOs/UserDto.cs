using clinics_api.services.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace clinics_api.services.Model.DTOs
{
    public class UserDto
    {
    }
    public class RegesterUserDto
    {
        [Required]

        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]

        public string PhoneNumber { get; set; }

        [Required]
        public string password { get; set; }


    }
    public class LoginDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]

        public string password { get; set; }


    }
    public class userAuth
    {

        public bool isAuth { get; set; }

        public string name { get; set; }
        public string Email { get; set; }
        public string token { get; set; }
        public string message { get; set; }

        public static explicit operator userAuth(ApplicationUser app)
        {
            return new userAuth
            {
                name = app.UserName,
                Email = app.Email,

            };
        }


    }
    public class RegesterDoctorUserDto
    {
        [Required]

        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]

        public string PhoneNumber { get; set; }

        [Required]
        public string password { get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public string Img { get; set; }
        [Required]

        public string Specialization { get; set; }
        [Required]

        public string LocationDetailes { get; set; }
        [Required]

        public TimeSpan OpeningTime { get; set; }
        [Required]

        public TimeSpan CloseTime { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public string fees { get; set; }
        [Required]

        public int DepartmentId { get; set; }
        [Required]

        public int LocationId { get; set; }
        [Required]

        public string UserId { get; set; }
    }


}
