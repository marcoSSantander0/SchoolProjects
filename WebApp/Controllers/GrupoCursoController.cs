using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GrupoCursoController : Controller
{
    private readonly GrupoCursoData _data;
    public GrupoCursoController(GrupoCursoData data) => _data = data;

    public IActionResult Edit(int id)
    {
        var entidad = _data.GetById(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost]
    public IActionResult Edit(GrupoCurso model)
    {
        if (!ModelState.IsValid) return View(model);
        _data.Update(model);
        return RedirectToAction("Index", "Admin");
    }

    public IActionResult Delete(int id)
    {
        _data.Delete(id);
        return RedirectToAction("Index", "Admin");
    }
}

}