using clinics_api.services.Model.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace clinics_api.services.Model.Interfaces
{
    public interface IUser
    {
        Task<bool> RegesterUserOrManager(RegesterUserDto user, ModelStateDictionary modelState,string role);
        Task<bool> RegesterDoctor(RegesterDoctorUserDto user, ModelStateDictionary modelState);
        Task<userAuth> Login(LoginDto user);

    }
}
