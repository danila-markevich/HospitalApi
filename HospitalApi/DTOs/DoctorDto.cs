namespace HospitalApi.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Specialty { get; set; } = "";
        public int AppointmentsCount { get; set; }
    }
}
