using clinics_api.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace clinics_api.Models.Interfaces
{
    public interface IDepartment
    {

        Task<bool> CreatDepartment(CreatDepartmentDto creatDepartmentDto);
        Task<bool> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto,int id);
        Task<bool> DeleteDepartment( int id);
        Task<List<DepartmentDto>> GetDepartments ();
        Task<List<LocationDto>> GetLocations();

        Task<DepartmentDoctorsDto> GetDoctorDepartment(int id);


    }
}
