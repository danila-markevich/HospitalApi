using HospitalApi.DTOs;

namespace HospitalApi.Services
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllAsync();
        Task<PatientDto> GetByIdAsync(int id);
        Task<PatientDto> CreateAsync(CreatePatientDto dto);
        Task UpdateAsync(int id, UpdatePatientDto dto);
        Task DeleteAsync(int id);
    }
}
