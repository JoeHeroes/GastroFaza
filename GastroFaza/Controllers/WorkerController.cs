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
        public async Task<IActionResult> GetAll()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var workers = await this.dbContext.Workers.ToListAsync();

                    var workersDto = new List<WorkerDTO>();

                    foreach(var worker in workers)
                    {

                        var role = this.dbContext.Roles.FirstOrDefault(x => x.Id== worker.RoleId);

                        workersDto.Add(new WorkerDTO()
                        {
                            Id= worker.Id,
                            Email= worker.Email,
                            FirstName= worker.FirstName,
                            LastName= worker.LastName,
                            DateOfBirth = worker.DateOfBirth,
                            Nationality= worker.Nationality,    
                            Rating= worker.Rating,
                            Role = role.Name
                        });
                    }
           

                    return View(workersDto);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> GetOne(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    if (id == null || id == 0)
                    {
                        return NotFound();
                    }

                    var worker = await this.dbContext.Workers.FirstOrDefaultAsync(x => x.Id == id);

                    return View(worker);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var worker = await this.dbContext.Workers.FindAsync(id);
                    if (worker == null)
                    {
                        return NotFound();
                    }

                    this.dbContext.Workers.Remove(worker);
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("GetAll");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var worker = await this.dbContext.Workers.FirstOrDefaultAsync(s => s.Id == Id);
                    var roles = await this.dbContext.Roles.ToListAsync();
                    ViewBag.data = roles;
                    return View(worker);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, UpdateWorkerDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = await this.dbContext.Workers.FindAsync(id);
                model.Salary = modelDTO.Salary;
                model.Rating = modelDTO.Rating;
                model.Email = modelDTO.Email;
                model.FirstName = modelDTO.FirstName;
                model.LastName = modelDTO.LastName;
                model.DateOfBirth = modelDTO.DateOfBirth;
                model.Nationality = modelDTO.Nationality;

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("GetAll");
            }

            return View(modelDTO);
        }

        public IActionResult RateWorker(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    return View();
                }
                return Forbid();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult RateWorker(int? id, int newRating)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Workers.Find(id);
                model.Rating = newRating;
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
            }
            return RedirectToAction("GetAll");
        }

    }
}