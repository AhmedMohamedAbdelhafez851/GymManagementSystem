using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        // Display the list of trainers
        public IActionResult List()
        {
            var trainers = _trainerService.GetAll();
            return View(trainers);
        }

        // Create a new trainer - Render Create View
        public IActionResult Create()
        {
            return View();
        }

        // Handle Create Trainer Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TrainerDTO trainerDto)
        {
            if (ModelState.IsValid)
            {
                _trainerService.Create(trainerDto);
                return RedirectToAction(nameof(List));
            }
            return View(trainerDto);
        }

        // Get trainer data for editing
        public IActionResult GetTrainer(int id)
        {
            var trainer = _trainerService.GetById(id);
            if (trainer == null) return NotFound();

            return Json(trainer);
        }

        // Handle Edit Trainer Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TrainerDTO trainerDto)
        {
            if (ModelState.IsValid)
            {
                _trainerService.Update(trainerDto);
               // return Json(new { success = true });
            }
            return RedirectToAction("List");
        }

        // Handle Delete Trainer
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var trainer = _trainerService.GetById(id);
            if (trainer == null) return Json(new { success = false, message = "Trainer not found" });

            _trainerService.Delete(id);
            //return Json(new { success = true });
            return RedirectToAction("List");

        }
    }
}
