using GastroFaza.Models.DTO;
using GastroFaza.Models;
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
                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
                {
                    IEnumerable<DiningTable> tables = this.dbContext.Tables;

                    return View(tables);
                }
                else
                {
                    return Forbid();
                }
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [Route("SearchTable")]
        public IActionResult SearchTable(string id)
        {
            if (id == null)
            {
                IEnumerable<DiningTable> tables = this.dbContext.Tables;

                return View(tables);
            }

            var baseQuery = dbContext.Tables.Where(x => x.Id == int.Parse(id));

            return View(baseQuery);
        }

        public IActionResult Edit(int Id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var table = this.dbContext.Tables.Where(s => s.Id == Id).FirstOrDefault();

                    return View(table);
                }
                else
                {
                    return Forbid();
                }
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public IActionResult Edit(int? id, TableDto modelDTO)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
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
                    }
                    return RedirectToAction("GetAll");
                }
                else
                {
                    return Forbid();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
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
                } else
                {
                    return Forbid();
                }
            } else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    return View();
                }
                else
                {
                    return Forbid();
                }
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        
        }

        [HttpPost]
        public IActionResult Create(TableDto modelDTO)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    this.dbContext.Tables.Add(new DiningTable()
                    {
                        Busy = false,
                        Seats = modelDTO.Seats,
                    });

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
                    return Forbid();
                }
            } else
            {
                return RedirectToAction("Login", "Account");
            }

            
        }


        public IActionResult SetBusy(int id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
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
                    return Forbid();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

    }
}
