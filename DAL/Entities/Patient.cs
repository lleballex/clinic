using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public enum PatientGender
    {
        Male, Female
    }

    [Table("patient")]
    public class Patient
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("surname")]
        [Required]
        public string Surname { get; set; }

        [Column("patronymic")]
        public string? Patronymic { get; set; }

        [Column("medical_policy_number")]
        [Required]
        public string MedicalPolicyNumber { get; set; }

        // TODO: maybe use DateOnly
        [Column("born_at")]
        [Required]
        public DateTime BornAt { get; set; }

        [Column("phone_number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Column("gender")]
        [Required]
        public PatientGender Gender { get; set; }

        [Column("house_id")]
        [Required]
        public int HouseId { get; set; }

        public House? House { get; set; }
    }
}
