using GastroFaza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class TableeController : Controller
    {
        private readonly RestaurantDbContext dbContext;

        public TableeController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult GetAll()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true")
                {
                    IEnumerable<Tablee> tables = this.dbContext.Tables;
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
    }
}
