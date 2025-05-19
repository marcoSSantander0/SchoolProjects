using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly InstructorData _instructorData;
        private readonly AlumnoData _alumnoData;
        private readonly CursoData _cursoData;
        private readonly GrupoData _grupoData;
        private readonly GrupoCursoData _grupoCursoData;
        private readonly GrupoCursoAlumnoData _grupoCursoAlumnoData;

        public AdminController(
            UserManager<IdentityUser> userManager,
            InstructorData instructorData,
            AlumnoData alumnoData,
            CursoData cursoData,
            GrupoData grupoData,
            GrupoCursoData grupoCursoData,
            GrupoCursoAlumnoData grupoCursoAlumnoData)
        {
            _userManager = userManager;
            _instructorData = instructorData;
            _alumnoData = alumnoData;
            _cursoData = cursoData;
            _grupoData = grupoData;
            _grupoCursoData = grupoCursoData;
            _grupoCursoAlumnoData = grupoCursoAlumnoData;
        }

        public async Task<IActionResult> Index()
        {
            // Usuarios y roles
            var users = _userManager.Users.ToList();
            var userRoles = new Dictionary<string, IList<string>>();
            foreach (var user in users)
                userRoles[user.Email] = await _userManager.GetRolesAsync(user);

            ViewBag.UserRoles = userRoles;

            // Otras entidades
            ViewBag.Instructores = _instructorData.GetAll();
            ViewBag.Alumnos = _alumnoData.GetAll();
            ViewBag.Cursos = _cursoData.GetAll();
            ViewBag.Grupos = _grupoData.GetAll();
            ViewBag.GrupoCursos = _grupoCursoData.GetAll();
            ViewBag.GrupoCursoAlumnos = _grupoCursoAlumnoData.GetAll();

            return View(users); // El modelo principal sigue siendo usuarios
        }
    }
}
