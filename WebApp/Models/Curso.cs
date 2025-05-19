using System.ComponentModel.DataAnnotations;
namespace WebApp.Models
{
    public class Curso
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}