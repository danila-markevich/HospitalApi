using HospitalApi.Data;
using HospitalApi.DTOs;
using HospitalApi.Exceptions;
using HospitalApi.Models;

using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HospitalApi.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService (ApplicationDbContext context)
        {
            _context = context;
        }

      
        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _context.Appointments
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name != null ? a.Doctor.Name : null,
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name != null ? a.Patient.Name : null,
                    Date = a.Date,
                    Diagnosis = a.Diagnosis,
                    Cost = a.Cost,
                })
                .ToListAsync();
            return appointments;
        }

        
        public async Task<List<AppointmentDto>> GetByDoctorAsync(int doctorId)
        {
            var doctorExist = await _context.Doctors.AnyAsync(d => d.Id == doctorId);
            if (!doctorExist)
                throw new NotFoundException($"Doctor with id {doctorId} not found");
            var appointmentsByDoctor = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name != null ? a.Doctor.Name : null,
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name != null ? a.Patient.Name : null,
                    Date = a.Date,
                    Diagnosis = a.Diagnosis,
                    Cost = a.Cost,
                })
                .ToListAsync();


            return appointmentsByDoctor;
        }

        
        public async Task<List<AppointmentDto>> GetByPatientAsync(int patientId)
        {
            var patientExist = await _context.Patients.AnyAsync(d => d.Id == patientId);
            if (!patientExist) 
                throw new NotFoundException($"Patient with id {patientId} not found");
            var appointmentsByPatient = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name != null ? a.Doctor.Name : null,
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name != null ? a.Patient.Name : null,
                    Date = a.Date,
                    Diagnosis = a.Diagnosis,
                    Cost = a.Cost,
                })
                .ToListAsync();


            return appointmentsByPatient;
        }

        
        public async Task<AppointmentDto> GetByIdAsync(int id)
        {
            var appointment = await _context.Appointments
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name != null ? a.Doctor.Name : null,
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name != null ? a.Patient.Name : null,
                    Date = a.Date,
                    Diagnosis = a.Diagnosis,
                    Cost = a.Cost,
                })
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
                throw new NotFoundException($"Appointment with id {id} not found");

            return appointment;
        }

        
        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
        {
            var doctor = await _context.Doctors.FindAsync(dto.DoctorId);
            if (doctor == null) 
                throw new BadRequestException($"Doctor with id {dto.DoctorId} not found");

            var patient = await _context.Patients.FindAsync(dto.PatientId);
            if (patient == null) 
                throw new BadRequestException($"Patient with id {dto.PatientId} not found");

            var appointment = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Date = dto.Date,
                Diagnosis = dto.Diagnosis,
                Cost = dto.Cost,
            };

            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            var result = new AppointmentDto
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                DoctorName = doctor.Name,
                PatientId = appointment.PatientId,
                PatientName = patient.Name,
                Date = appointment.Date,
                Diagnosis = appointment.Diagnosis,
                Cost = appointment.Cost,
            };

            return result;
        }

        
        public async Task UpdateAsync(int id, UpdateAppointmentDto dto)
        {
            var existing = await _context.Appointments.FindAsync(id);
            if (existing == null) 
                throw new NotFoundException($"Appointment with id {id} not found");

            if (dto.DoctorId != existing.DoctorId)
            {
                var doctorExist = await _context.Doctors.AnyAsync(d => d.Id == dto.DoctorId);
                if (!doctorExist) 
                    throw new BadRequestException($"Doctor with id {dto.DoctorId} not found");
            }

            if (dto.PatientId != existing.PatientId)
            {
                var patientExist = await _context.Patients.AnyAsync(p => p.Id == dto.PatientId);
                if (!patientExist) 
                    throw new BadRequestException($"Patient with id {dto.PatientId} not found");
            }

            existing.DoctorId = dto.DoctorId;
            existing.PatientId = dto.PatientId;
            existing.Date = dto.Date;
            existing.Diagnosis = dto.Diagnosis;
            existing.Cost = dto.Cost;

            await _context.SaveChangesAsync();
            
        }

        
        public async Task DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) 
                throw new NotFoundException($"Appointment with id {id} not found");

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            
        }
    }
}
