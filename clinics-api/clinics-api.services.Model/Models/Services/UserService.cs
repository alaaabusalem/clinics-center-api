

using clinics_api.services.Model.DTOs;
using clinics_api.services.Model.Interfaces;
using clinics_api.services.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace clinics_api.services.Model.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly ClinicsDbContext _Db;

        public UserService(UserManager<ApplicationUser> userManager, JwtTokenService jwtTokenService,ClinicsDbContext db)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService; 
            _Db = db;

        }
        public async Task<userAuth> Login(LoginDto user)
        {
            if (user == null) return null;
            
              var userResult=await _userManager.FindByEmailAsync(user.Email);

            if (userResult!= null && await _userManager.CheckPasswordAsync(userResult,user.password)) {
                var userauth = (userAuth)userResult;
                string token = await _jwtTokenService.GetToken(userResult, TimeSpan.FromDays(1));
                userauth.isAuth=true;
                userauth.token=token;
                return userauth;
            }
            
            var userauthNotAuth = new userAuth();

            userauthNotAuth.isAuth = false;
            userauthNotAuth.message = "Wrong Email Or Passoword";
            return userauthNotAuth;    
            
        }

        public async Task<bool> RegesterUserOrManager(RegesterUserDto user, ModelStateDictionary modelState, string role)
        {
            var appUser = new ApplicationUser();
            //if(await _userManager.FindByEmailAsync(user.Email)=) { }
             appUser = (ApplicationUser)user;
           var result = await _userManager.CreateAsync(appUser,user.password);
            if(result.Succeeded) {
                await _userManager.AddToRoleAsync(appUser, role);
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
        public async Task<bool> RegesterDoctor(RegesterDoctorUserDto user, ModelStateDictionary modelState)
        {
            var appUser = new ApplicationUser();
            //if(await _userManager.FindByEmailAsync(user.Email)=) { }
            appUser = (ApplicationUser)user;
            var result = await _userManager.CreateAsync(appUser, user.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "Doctor");

                var doctor = (Doctor)user;
                doctor.UserId = appUser.Id;
                await _Db.Doctors.AddAsync(doctor);
                await _Db.SaveChangesAsync();
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
