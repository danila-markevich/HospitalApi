using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs
{
    public class UpdateAppointmentDto
    {
        [Range(1, int.MaxValue)]
        public int DoctorId { get; set; }

        [Range(1, int.MaxValue)]
        public int PatientId { get; set; }

        public DateTime Date { get; set; }

        [StringLength(500)]
        public string Diagnosis { get; set; } = "";

        [Range(0, 1000000)]
        public decimal Cost { get; set; }
    }
}
