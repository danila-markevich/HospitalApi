using HospitalApi.DTOs;

namespace HospitalApi.Services
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllAsync();
        Task<DoctorDto> GetByIdAsync(int id);
        Task<DoctorDto> CreateAsync(CreateDoctorDto dto);
        Task UpdateAsync(int id, UpdateDoctorDto dto);
        Task DeleteAsync(int id);
    }
}