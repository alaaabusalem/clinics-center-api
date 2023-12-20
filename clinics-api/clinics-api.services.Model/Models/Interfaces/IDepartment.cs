using clinics_api.services.Model.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace clinics_api.services.Model.Interfaces
{
    public interface IDepartment
    {

        Task<bool> CreatDepartment(CreatDepartmentDto creatDepartmentDto);
        Task<bool> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto,int id);
        Task<bool> DeleteDepartment( int id);
        Task<List<DepartmentDto>> GetDepartments ();
        Task<DepartmentDoctorsDto> GetDoctorDepartment(int id);


    }
}
