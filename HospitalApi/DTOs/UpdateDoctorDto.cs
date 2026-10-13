using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs
{
    public class UpdateDoctorDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Specialty { get; set; } = "";

    }
}
