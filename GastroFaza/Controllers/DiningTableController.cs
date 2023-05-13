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

        public async Task<IActionResult> GetAll()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
                {
                    var tables = await this.dbContext.Tables.ToListAsync();

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
        public async Task<IActionResult> SearchTable(int id)
        {
            if (id == 0)
            {
                var tables  = await this.dbContext.Tables.ToListAsync();

                return View(tables);
            }

            var baseQuery = await this.dbContext.Tables.Where(x => x.Id == id).ToListAsync();

            return View(baseQuery);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var table = await this.dbContext.Tables.FirstOrDefaultAsync(s => s.Id == Id);


                    var tableDto = new TableDto()
                    {
                        Seats = table.Seats
                    };

                    return View(tableDto);
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
        public async Task<IActionResult> Edit(int? id, TableDto modelDTO)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    if (ModelState.IsValid)
                    {
                        var model =  await this.dbContext.Tables.FindAsync(id);
                        model.Seats = modelDTO.Seats;

                        try
                        {
                            await this.dbContext.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var table = await this.dbContext.Tables.FindAsync(id);
                    if (table == null)
                    {
                        return NotFound();
                    }

                    this.dbContext.Tables.Remove(table);
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
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
        public async Task<IActionResult> Create(TableDto modelDTO)
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
                        await this.dbContext.SaveChangesAsync();
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


        public async Task<IActionResult> SetBusy(int id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
                {
                    var table = await this.dbContext.Tables.FindAsync(id);
                    table.Busy = !table.Busy;
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
