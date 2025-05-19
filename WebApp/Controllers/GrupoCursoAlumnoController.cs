using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GrupoCursoAlumnoController : Controller
{
    private readonly GrupoCursoAlumnoData _data;
    public GrupoCursoAlumnoController(GrupoCursoAlumnoData data) => _data = data;

    public IActionResult Delete(int grupoCursoId, int alumnoId)
    {
        _data.Delete(grupoCursoId, alumnoId);
        return RedirectToAction("Index", "Admin");
    }
}

}