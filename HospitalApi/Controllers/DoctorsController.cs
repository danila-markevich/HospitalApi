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
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DoctorDto>>> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();
            return doctors;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetById(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            return doctor;
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> Create(CreateDoctorDto dto)
        {
            var result = await _doctorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDoctorDto dto)
        {
            await _doctorService.UpdateAsync(id, dto);
            return NoContent();  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _doctorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
