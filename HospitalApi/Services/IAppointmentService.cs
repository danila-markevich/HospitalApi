using HospitalApi.DTOs;

namespace HospitalApi.Services
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto> GetByIdAsync(int id);
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);
        Task UpdateAsync(int id, UpdateAppointmentDto dto);
        Task DeleteAsync(int id);
        Task<List<AppointmentDto>> GetByDoctorAsync(int doctorId);
        Task<List<AppointmentDto>> GetByPatientAsync(int patientId);
    }
}
