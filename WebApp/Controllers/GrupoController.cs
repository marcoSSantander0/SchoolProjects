using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GrupoController : Controller
{
    private readonly GrupoData _grupoData;
    public GrupoController(GrupoData data) => _grupoData = data;

    public IActionResult Edit(int id)
    {
        var grupo = _grupoData.GetById(id);
        if (grupo == null) return NotFound();
        return View(grupo);
    }

    [HttpPost]
    public IActionResult Edit(Grupo model)
    {
        if (!ModelState.IsValid) return View(model);
        _grupoData.Update(model);
        return RedirectToAction("Index", "Admin");
    }

    public IActionResult Delete(int id)
    {
        _grupoData.Delete(id);
        return RedirectToAction("Index", "Admin");
    }
}

}