using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class DishController : Controller
    {
        private readonly RestaurantDbContext dbContext;

        public DishController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult GetAll()
        {
            IEnumerable<Dish> dishs = this.dbContext.Dishs;

            return View(dishs);
        }

        public IActionResult GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var dish = this.dbContext.Dishs.Find(id);

            return View(dish);
        }
        public IActionResult Delete(int? id)
        {
            var dish = this.dbContext.Dishs.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            this.dbContext.Dishs.Remove(dish);
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

        public IActionResult Edit(int? id, DishDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Dishs.Find(id);
                model.Name = modelDTO.Name;
                model.Description = modelDTO.Description;
                model.Price = modelDTO.Price;

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

        public IActionResult Create(DishDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                this.dbContext.Dishs.Add(new Dish()
                {
                    Name = modelDTO.Name,
                    Description = modelDTO.Description,
                    Price = modelDTO.Price,
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
