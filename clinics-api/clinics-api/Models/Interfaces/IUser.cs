﻿using clinics_api.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace clinics_api.Models.Interfaces
{
    public interface IUser
    {
        Task<bool> RegesterUserOrManager(RegesterUserDto user, ModelStateDictionary modelState,string role);
        Task<int> RegesterDoctor(RegesterDoctorUserDto user, ModelStateDictionary modelState);
        Task<userAuth> Login(LoginDto user);
        Task<bool> RegesterDoctorImg(int doctorId, IFormFile file);


    }
}
