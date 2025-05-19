using System.ComponentModel.DataAnnotations;
namespace WebApp.Models
{
   public class GrupoCurso
    {
        public int Id { get; set; }

        public int GrupoId { get; set; }

        public int CursoId { get; set; }

        public int InstructorId { get; set; }
    }
}