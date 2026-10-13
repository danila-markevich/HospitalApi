using HospitalApi.Data;
using HospitalApi.DTOs;
using HospitalApi.Exceptions;
using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorDto>> GetAllAsync()
        {
            var doctors = await _context.Doctors
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Specialty = d.Specialty,
                    AppointmentsCount = d.Appointments.Count()
                })
                .ToListAsync();

            return doctors;
        }

        public async Task<DoctorDto> GetByIdAsync(int id)
        {
            var doctor = await _context.Doctors
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Specialty = d.Specialty,
                    AppointmentsCount = d.Appointments.Count()
                })
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new NotFoundException($"Doctor with id {id} not found");

            return doctor;   
        }

        public async Task<DoctorDto> CreateAsync(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                Specialty = dto.Specialty
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            var result = new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                AppointmentsCount = 0
            };

            return result;
        }

        public async Task UpdateAsync(int id, UpdateDoctorDto dto)
        {
            var existing = await _context.Doctors.FindAsync(id);
            if (existing == null)
                throw new NotFoundException($"Doctor with id {id} not found");

            existing.Name = dto.Name;
            existing.Specialty = dto.Specialty;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new NotFoundException($"Doctor with id {id} not found");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
