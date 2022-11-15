using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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



        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateDishDto model, [FromRoute] int id)
        {
            this.services.Update(id, model);

            return Ok();
        }



        [HttpPost]
        public ActionResult Create([FromBody] UpdateDishDto dto)
        {

            //HttpContext.Workers.IsInRole("Admin");
            //var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = this.services.Create(dto);

            return Created($"/api/bid/{id}", null);
        }

    }
}
