using HospitalApi.Data;
using HospitalApi.DTOs;
using HospitalApi.Exceptions;
using HospitalApi.Models;

using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HospitalApi.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService (ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<PatientDto>> GetAllAsync()
        {
            var patients = await _context.Patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Phone = p.Phone,
                    AppointmentsCount = p.Appointments.Count(),
                })
                .ToListAsync();

            return patients;
        }

        
        public async Task<PatientDto> GetByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Phone = p.Phone,
                    AppointmentsCount = p.Appointments.Count(),
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                throw new NotFoundException($"Patient with id {id} not found");

            return patient;
        }

      
        public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                Name = dto.Name,
                Age = dto.Age,
                Phone = dto.Phone,
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            var result = new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Age = patient.Age,
                Phone = patient.Phone,
                AppointmentsCount = 0
            };

            return result;

        }

        public async Task UpdateAsync(int id, UpdatePatientDto dto)
        {
            var existing = await _context.Patients.FindAsync(id);
            if (existing == null) 
                throw new NotFoundException($"Patient with id {id} not found");

            existing.Name = dto.Name;
            existing.Age = dto.Age;
            existing.Phone = dto.Phone;

            await _context.SaveChangesAsync();            

        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) 
                throw new NotFoundException($"Patient with id {id} not found"); 

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            

        }
    }
}
