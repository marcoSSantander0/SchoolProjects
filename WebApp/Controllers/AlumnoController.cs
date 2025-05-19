using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AlumnoController : Controller
    {
        private readonly AlumnoData _alumnoData;

        public AlumnoController(AlumnoData data) => _alumnoData = data;

        // GET: /Alumno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Alumno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Alumno model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _alumnoData.Create(model);
            return RedirectToAction("Index", "Admin");
        }

        // GET: /Alumno/Edit/{id}
        public IActionResult Edit(int id)
        {
            var alumno = _alumnoData.GetById(id);
            if (alumno == null)
                return NotFound();

            return View(alumno);
        }

        // POST: /Alumno/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Alumno model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _alumnoData.Update(model);
            return RedirectToAction("Index", "Admin");
        }

        // GET: /Alumno/Delete/{id}
        public IActionResult Delete(int id)
        {
            var alumno = _alumnoData.GetById(id);
            if (alumno == null)
                return NotFound();

            return View(alumno);
        }

        // POST: /Alumno/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _alumnoData.Delete(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}
