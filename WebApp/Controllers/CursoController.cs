using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CursoController : Controller
    {
        private readonly CursoData _cursoData;

        public CursoController(CursoData data) => _cursoData = data;

        // GET: /Curso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Curso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Curso model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _cursoData.Create(model);
            return RedirectToAction("Index", "Admin");
        }

        // GET: /Curso/Edit/{id}
        public IActionResult Edit(int id)
        {
            var curso = _cursoData.GetById(id);
            if (curso == null)
                return NotFound();

            return View(curso);
        }

        // POST: /Curso/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Curso model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _cursoData.Update(model);
            return RedirectToAction("Index", "Admin");
        }

        // GET: /Curso/Delete/{id}
        public IActionResult Delete(int id)
        {
            var curso = _cursoData.GetById(id);
            if (curso == null)
                return NotFound();

            return View(curso);
        }

        // POST: /Curso/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _cursoData.Delete(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}
