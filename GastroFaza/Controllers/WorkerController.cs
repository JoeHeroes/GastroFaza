using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class WorkerController : Controller
    {
        private readonly RestaurantDbContext dbContext;

        public WorkerController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult GetAll()
        {
            IEnumerable<Worker> workers = this.dbContext.Workers;

            return View(workers);
        }

        public IActionResult GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var worker = this.dbContext.Workers.Find(id);

            return View(worker);
        }

        public IActionResult Delete(int? id)
        {
            var worker = this.dbContext.Workers.Find(id);
            if (worker == null)
            {
                return NotFound();
            }

            this.dbContext.Workers.Remove(worker);
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return RedirectToAction("GetAll");
        }
        public IActionResult Edit(int Id)
        {
            var worker = this.dbContext.Workers.Where(s => s.Id == Id).FirstOrDefault();
            IEnumerable<Role> roles = this.dbContext.Roles;
            ViewBag.data = roles;
            return View(worker);
        }
        [HttpPost]
        public IActionResult Edit(int? id, UpdateWorkerDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Workers.Find(id);
                model.Salary = modelDTO.Salary;
                model.Rating = modelDTO.Rating;
                model.Email = modelDTO.Email;
                model.FirstName = modelDTO.FirstName;
                model.LastName = modelDTO.LastName;
                model.DateOfBirth = modelDTO.DateOfBirth;
                model.Nationality = modelDTO.Nationality;

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("GetAll");
            }

            return View(modelDTO);
        }
        
    }
}