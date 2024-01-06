using clinics_api.Models.DTOs;

namespace clinics_api.Models.Interfaces
{
    public interface IAppointment
    {
        Task<bool> CreateAppointment(CreatAppointmentDto creatAppointmentDto, string userId );
        Task<List<AppointmentDto>> UserAppointments(string userId);
        Task<List<DoctorAppointmentDto>> DoctorAppointments(string userId);


    }
}
