using clinics_api.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace clinics_api.Models.Interfaces
{
    public interface IDoctor
    {
        Task<DoctorBookDto> GetDocDateSlots(int doctorId);
        Task<List<DateSlot>> GetDateSlots(int doctorId);

        Task<List<DoctorBookDto>> GetDocBySpecialization(int departmentId);
        Task<List<DoctorBookDto>> GetDocByLocation(int locationId);
        Task<List<DoctorBookDto>> GetDocByLocationAndSpecialization(int locationId, int departmentId);


    }

}
