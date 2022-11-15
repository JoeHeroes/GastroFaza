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
        public IActionResult GetAll()
        {
            IEnumerable<Restaurant> restaurants = this.dbContext.Restaurants;

            return View(restaurants);
        }

        public IActionResult GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var restaurant = this.dbContext.Restaurants.Find(id);

            return View(restaurant);
        }
        public IActionResult Delete(int? id)
        {
            var restaurant = this.dbContext.Restaurants.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            this.dbContext.Restaurants.Remove(restaurant);
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

        public IActionResult Edit(int? id, UpdateRestaurantDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Restaurants.Find(id);
                model.Name = modelDTO.Name;
                model.HasDelivery = modelDTO.HasDelivery;
                model.Description = modelDTO.Description;

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

        public IActionResult Create(CreateRestaurantDto modelDTO)
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
