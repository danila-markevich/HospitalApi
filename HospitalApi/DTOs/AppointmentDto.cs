namespace HospitalApi.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public int PatientId { get; set; }
        public string? PatientName { get; set; }
        public DateTime Date { get; set; }
        public string Diagnosis { get; set; } = "";
        public decimal Cost { get; set; }

    }
}
