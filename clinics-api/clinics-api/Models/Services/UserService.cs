using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace clinics_api.Models.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager; 
        }
        public async Task<userAuth> Login(LoginDto user)
        {
            if (user == null) return null;
            
              var userResult=await _userManager.FindByEmailAsync(user.Email);

            if (userResult!= null && await _userManager.CheckPasswordAsync(userResult,user.password)) {
                var userauth = (userAuth)userResult;

                userauth.isAuth=true;
                return userauth;
            }
            
            var userauthNotAuth = new userAuth();

            userauthNotAuth.isAuth = false;
            userauthNotAuth.message = "Wrong Email Or Passoword";
            return userauthNotAuth;    
            
        }

        public async Task<bool> RegesterUser(RegesterUserDto user, ModelStateDictionary modelState)
        {
            var appUser = new ApplicationUser();
            //if(await _userManager.FindByEmailAsync(user.Email)=) { }
             appUser = (ApplicationUser)user;
           var result = await _userManager.CreateAsync(appUser,user.password);
            if(result.Succeeded) {
                return true;
            
            }
            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(user.password) :
                         error.Code.Contains("Email") ? nameof(user.Email) :
                         error.Code.Contains("Username") ? nameof(user.name) :
                         error.Code.Contains("PhoneNumber") ? nameof(user.PhoneNumber) :

                         "";

                modelState.AddModelError(errorKey, error.Description);
            }
            return false;   
        }
    }
}
