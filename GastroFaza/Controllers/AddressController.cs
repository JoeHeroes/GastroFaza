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
            IEnumerable<Address> addresses = this.dbContext.Addresses;

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
            var address = this.dbContext.Addresses.FindAsync(id);
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

        public async Task<IActionResult> Edit(int? id, AddressDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Addresses.Find(id);
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

        public async Task<IActionResult> Create(AddressDto modelDTO)
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
    }
}
