using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [StringLength(100)]
        public string Especialidad { get; set; }
    }
}