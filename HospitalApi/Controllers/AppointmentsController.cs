using HospitalApi.Data;
using HospitalApi.DTOs;
using HospitalApi.Models;
using HospitalApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentDto>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return appointments;
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<List<AppointmentDto>>> GetByDoctor(int doctorId)
        {
            var result = await _appointmentService.GetByDoctorAsync(doctorId);            
            return result;
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<AppointmentDto>>> GetByPatient(int patientId)
        {
            var result = await _appointmentService.GetByPatientAsync(patientId);           
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById (int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);            
            return appointment;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create (CreateAppointmentDto dto)
        {
            var result = await _appointmentService.CreateAsync(dto);            
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update (int id, UpdateAppointmentDto dto)
        {
            await _appointmentService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAsync(id);
            return NoContent();
        }

        

        
    }
}
