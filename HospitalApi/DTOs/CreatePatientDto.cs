using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs
{
    public class CreatePatientDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = "";

        [Range(0, 150)]
        public int Age { get; set; }

        [StringLength(20)]
        public string Phone { get; set; } = "";

    }
}
