using GastroFaza.Models;
using GastroFaza.Models.DTO;
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
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id, UpdateWorkerDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Workers.Find(id);
                model.Salary = modelDTO.Salary;
                model.Rating = modelDTO.Rating;
                model.Email = modelDTO.Email;

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Index");
            }

            return View(modelDTO);
        }
        public IActionResult Create(CreateWorkerDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                this.dbContext.Workers.Add(new Worker()
                {
                    Email = modelDTO.Email,
                    FirstName = modelDTO.FirstName,
                    LastName = modelDTO.LastName,
                    DateOfBirth = modelDTO.DateOfBirth,
                    Nationality = modelDTO.Nationality,
                    PasswordHash = modelDTO.PasswordHash,
                    Salary = modelDTO.Salary,
                    Rating = modelDTO.Rating
                });

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                return RedirectToAction("Index");
            }

            return View(modelDTO);
        }
    }
}