using System.ComponentModel.DataAnnotations;
namespace WebApp.Models
{
   public class Alumno
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

        [Required]
        [EmailAddress]
    public string Correo { get; set; }

}

}