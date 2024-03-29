﻿using System.ComponentModel.DataAnnotations;

namespace clinics_api.Models.DTOs
{
    public class UserDto
    {
        public string userId { get; set; }

        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]

        public string PhoneNumber { get; set; }

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
        public string _token { get; set; }
        public string expired { get; set; }
        public string role { get; set; }

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

        public string Specialization { get; set; }
        [Required]

        public string LocationDetailes { get; set; }

        [Required]

        public string OpeningTime { get; set; }

        [Required]

        public string CloseTime { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public string fees { get; set; }
        [Required]

        public int DepartmentId { get; set; }
        [Required]

        public int LocationId { get; set; }

    }


}
