using System.ComponentModel.DataAnnotations;
namespace WebApp.Models
{
   public class Grupo
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    public string Turno { get; set; }  // Matutino, Vespertino, etc.
}

}