using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructorController : Controller
    {
        private readonly InstructorData _instructorData;

        public InstructorController(InstructorData data) => _instructorData = data;

        // GET: /Instructor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Instructor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instructor model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _instructorData.Create(model);
            return RedirectToAction("Index", "Admin");
        }

        // GET: /Instructor/Edit/{id}
        public IActionResult Edit(int id)
        {
            var instructor = _instructorData.GetById(id);
            if (instructor == null)
                return NotFound();

            return View(instructor);
        }

        // POST: /Instructor/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Instructor model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _instructorData.Update(model);
            return RedirectToAction("Index", "Admin");
        }

        // GET: /Instructor/Delete/{id}
        public IActionResult Delete(int id)
        {
            var instructor = _instructorData.GetById(id);
            if (instructor == null)
                return NotFound();

            return View(instructor); // Vista de confirmación
        }

        // POST: /Instructor/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _instructorData.Delete(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}
