using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITrainerService _trainerService;

        public ClassesController(IClassService classService, ITrainerService trainerService)
        {
            _classService = classService;
            _trainerService = trainerService;
        }

        public IActionResult List()
        {
            var classes = _classService.GetAll();
            return View(classes);
        }

        public IActionResult Create()
        {
            ViewBag.Trainers = _trainerService.GetAll();
            return View(new ClassDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassDTO classDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Trainers = _trainerService.GetAll();
                return View(classDto);
            }

            _classService.Create(classDto);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult EditModal(int id)
        {
            var classDto = _classService.GetById(id);
            if (classDto == null)
                return NotFound();

            var trainers = _trainerService.GetAll();
            return Json(new { classDto, trainers });
        }

        [HttpPost]
        public IActionResult Edit(ClassDTO classDto)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid input" });

            _classService.Update(classDto);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _classService.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
