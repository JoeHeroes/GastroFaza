using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly RestaurantDbContext dbContext;

        public RestaurantController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await this.dbContext.Restaurants.ToListAsync();

            return View(restaurants);
        }

        public async Task<IActionResult> GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var restaurant = await this.dbContext.Restaurants.FindAsync(id);

            return View(restaurant);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var restaurant = await this.dbContext.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            this.dbContext.Restaurants.Remove(restaurant);
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
        public async Task<IActionResult> Edit(int Id)
        {
            var restaurant = await this.dbContext.Restaurants.FirstOrDefaultAsync();

            return View(restaurant);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, UpdateRestaurantDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = await this.dbContext.Restaurants.FindAsync(id);
                model.Name = modelDTO.Name;
                model.HasDelivery = modelDTO.HasDelivery;
                model.Description = modelDTO.Description;
                model.ContactEmail = modelDTO.ContactEmail;
                model.ContactNumber = modelDTO.ContactNumber;

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("RestaurantDetails");
            }

            return View(modelDTO);
        }
        public async Task<IActionResult> RestaurantDetails(CreateRestaurantDto modelDTO)
        {
            var restaurant = await this.dbContext.Restaurants.FirstOrDefaultAsync();
            return View(restaurant);
        }
        public async Task<IActionResult> Create(CreateRestaurantDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                this.dbContext.Restaurants.Add(new Restaurant()
                {
                    Name = modelDTO.Name,
                    Description = modelDTO.Description,
                    HasDelivery = modelDTO.HasDelivery,
                    ContactEmail = modelDTO.ContactEmail,
                    ContactNumber = modelDTO.ContactNumber,
                    Address = new Address()
                    {
                        City = modelDTO.City,
                        Street = modelDTO.Street,
                        PostalCode = modelDTO.PostalCode
                    }
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
