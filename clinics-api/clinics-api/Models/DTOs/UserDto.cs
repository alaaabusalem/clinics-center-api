using System.ComponentModel.DataAnnotations;

namespace clinics_api.Models.DTOs
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


}
