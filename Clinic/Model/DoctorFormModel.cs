using DAL.Entities;

namespace Clinic.Model
{
    public class DoctorFormModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public UserGender Gender { get; set; }
        public DateTime BornAt { get; set; }
        public TimeOnly AppointmentDuration { get; set; }
        public int SpecializationId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
