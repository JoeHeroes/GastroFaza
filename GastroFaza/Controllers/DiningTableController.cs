using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class DiningTableController : Controller
    {
        private readonly RestaurantDbContext dbContext;

        public DiningTableController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult GetAll()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true")
                {
                    IEnumerable<DiningTable> tables = this.dbContext.Tables;
                    return View(tables);
                }
                else
                {
                    return RedirectToAction("Welcome","Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }


        public IActionResult SetBusy(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true")
                {
                    var table = this.dbContext.Tables.Find(id);
                    table.Busy = !table.Busy;
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
                else
                {
                    return RedirectToAction();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Create(TableDto modelDTO)
        {

            if (ModelState.IsValid)
            {
                this.dbContext.Tables.Add(new DiningTable()
                {
                    Busy = false,
                    Seats  = modelDTO.Seats,
                });

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


        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Edit(int? id, TableDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Tables.Find(id);
                model.Seats = modelDTO.Seats;
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

        public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                var table = this.dbContext.Tables.Find(id);
                if (table == null)
                {
                    return NotFound();
                }

                this.dbContext.Tables.Remove(table);
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
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
