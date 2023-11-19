using clinics_api.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace clinics_api.Models.Interfaces
{
    public interface IUser
    {
        Task<bool> RegesterUser(RegesterUserDto user, ModelStateDictionary modelState);
        Task<userAuth> Login(LoginDto user);

    }
}
