using clinics_api.Models.DTOs;

namespace clinics_api.Models.Interfaces
{
    public interface IAppointment
    {
        Task<bool> CreateAppointment(CreatAppointmentDto creatAppointmentDto, string userId );
        Task<List<AppointmentDto>> UserAppointments(string userId);
        Task<List<DoctorAppointmentDto>> DoctorAppointments(string userId);
        Task<UpdateAppointmentDto> GetDoctorAppointment(string userId, int appointmentIdd);

        Task<List<AppointmentStatusDto>> AppointmentStatusList();
        Task<List<AppointmentDto>> UserAppointmentsByStatus(string userId, int appointmentStatusId);
        Task<bool> UpdateAppointment(string userId, UpdateAppointmentDto updateAppointmentDto);

        Task<List<DoctorAppointmentDto>> DoctorAppointmentsByStatus(string userId, int appointmentStatusId);
        //Task<List<DoctorAppointmentDto>> DoctorAppointmentsBydateAndStatus(string userId, int appointmentStatusId,string date);
        //Task<List<DoctorAppointmentDto>> DoctorAppointmentsBydate(string userId, string date );


    }
}
