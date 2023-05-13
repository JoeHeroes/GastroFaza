using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class AddressController : Controller
    {
        private readonly RestaurantDbContext dbContext;
        public AddressController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> GetAll()
        {
            var addresses = await this.dbContext.Addresses.ToListAsync();

            return View(addresses);
        }
        public async Task<IActionResult> GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var address = await this.dbContext.Addresses.FindAsync(id);

            return View(address);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {

                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") == "Manager")
                {

                    var address = await this.dbContext.Addresses.FindAsync(id);
                    if (address == null)
                    {

                        return NotFound();
                    }

                    this.dbContext.Addresses.Remove(address);
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("Index");

                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Edit(int? id, AddressDto modelDTO)
        {
            if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") == "Manager")
            {
                if (ModelState.IsValid)
                {
                    var model = await this.dbContext.Addresses.FindAsync(id);
                    model.City = modelDTO.City;
                    model.Street = modelDTO.Street;
                    model.PostalCode = modelDTO.PostalCode;

                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }

                    return RedirectToAction("Index");
                }

                return View(modelDTO);
            }
            return Forbid();
        }

        public async Task<IActionResult> Create(AddressDto modelDTO)
        {
            if (HttpContext.Session.GetString("email") != null)
            {

                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") == "Manager")
                {
                    if (ModelState.IsValid)
                    {
                        this.dbContext.Addresses.Add(new Address()
                        {
                            City = modelDTO.City,
                            Street = modelDTO.Street,
                            PostalCode = modelDTO.PostalCode,
                        });

                        try
                        {
                            await this.dbContext.SaveChangesAsync();
                        }
                        catch (DbUpdateException e)
                        {
                            throw new DbUpdateException("Error DataBase", e);
                        }
                        return RedirectToAction("Index");
                    }

                    return View(modelDTO);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
