using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public enum UserRole
    {
        Doctor, Admin, Registrar
    }

    public enum UserGender
    {
        Male, Female
    }

    [Table("user")]
    public class User
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("role")]
        [Required]
        public UserRole Role { get; set; }

        [Column("email")]
        [Required]
        // TODO: unique
        public string Email { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("surname")]
        [Required]
        public string Surname { get; set; }

        [Column("patronymic")]
        public string? Patronymic { get; set; }

        [Column("gender")]
        [Required]
        public UserGender Gender { get; set; }

        [Column("born_at")]
        [Required]
        public DateTime BornAt {  get; set; }

        public DoctorProfile? Doctor { get; set; }
    }
}
