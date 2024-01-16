using clinics_api.Data;
using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace clinics_api.Models.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly ClinicsDbContext _Db;

        public UserService(UserManager<ApplicationUser> userManager, JwtTokenService jwtTokenService, ClinicsDbContext db)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _Db = db;

        }
        public async Task<userAuth> Login(LoginDto user)
        {
            if (user == null) return null;

            var userResult = await _userManager.FindByEmailAsync(user.Email);

            if (userResult != null && await _userManager.CheckPasswordAsync(userResult, user.password))
            {
                var userauth = (userAuth)userResult;
                string token = await _jwtTokenService.GetToken(userResult, TimeSpan.FromMinutes(30));
                userauth.expired = DateTime.Now.AddMinutes(30).ToString("M/d/yyyy h:mm:ss tt");
                userauth._token = token;
                IList<string> list = await _userManager.GetRolesAsync(userResult);
                userauth.role = list[0];
                return userauth;
            }
            else
            {
                return null;
            }


        }

        public async Task<bool> RegesterUserOrManager(RegesterUserDto user, ModelStateDictionary modelState, string role)
        {
            var appUser = new ApplicationUser();
            //if(await _userManager.FindByEmailAsync(user.Email)=) { }
            appUser = (ApplicationUser)user;
            var result = await _userManager.CreateAsync(appUser, user.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, role);
                return true;

            }
            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(user.password) :
                         error.Code.Contains("Email") ? nameof(user.Email) :
                         error.Code.Contains("UserName") ? nameof(user.name) :
                         error.Code.Contains("PhoneNumber") ? nameof(user.PhoneNumber) :

                         "";

                modelState.AddModelError(errorKey, error.Description);
            }
            return false;
        }
        public async Task<int> RegesterDoctor(RegesterDoctorUserDto user, ModelStateDictionary modelState)
        {
            var appUser = new ApplicationUser();
            //if(await _userManager.FindByEmailAsync(user.Email)=) { }
            appUser = (ApplicationUser)user;
            var result = await _userManager.CreateAsync(appUser, user.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "Doctor");

                    var doctor = (Doctor)user;
                    doctor.Img = "";
                    doctor.UserId = appUser.Id;
                    await _Db.Doctors.AddAsync(doctor);
                    await _Db.SaveChangesAsync();
                    return doctor.DoctorId;
                
               


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
            return -1;
        }

        public async Task<bool> RegesterDoctorImg(int doctorId, IFormFile imgForm)
        {

            var doctor = await _Db.Doctors.FindAsync(doctorId);
            if (doctor != null && imgForm != null && imgForm.Length != 0)
            {
                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imgForm.FileName);

                // Define the path where the file will be saved
                var filePath = Path.Combine("wwwroot/images", fileName);

                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imgForm.CopyToAsync(stream);
                }
                doctor.Img = fileName;
                _Db.Entry(doctor).State = EntityState.Modified;
                await _Db.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
