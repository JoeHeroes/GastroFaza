using GastroFaza.Models;
using GastroFaza.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class OrderController : Controller
    {
        private readonly RestaurantDbContext dbContext;

        public OrderController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
       
        [Route("Order")]
        public IActionResult Order()
        {
            if (HttpContext.Session.GetString("current order") == null)
            {
                return RedirectToAction("Create");
            }
            int id = int.Parse(HttpContext.Session.GetString("current order"));

            var dishes = this.dbContext.Orders.Where(o => o.Id == id).SelectMany(o => o.Dishes);

            return View(dishes);
        }
        public IActionResult RemoveDishFromOrder(Order order, Dish dish)
        {
            //ToDo Zrobić usuwanie w relacji ManyToMany
            //this.dbContext.Orders.FirstOrDefault(u=>u.Id==order.Id).Dishes.Remove(dish);


            var lol = this.dbContext.Orders.FirstOrDefault(u => u.Id == order.Id);

            lol.Dishes.Remove(dish);

            try
            {
                this.dbContext.SaveChanges();
                HttpContext.Session.SetString("current order", this.dbContext.Orders.FirstOrDefault(u => u == order).Id.ToString());
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return View();
        }

        [Route("Create")]
        public IActionResult Create()
        {
            //if(this.dbContext.Orders.ToList().Count > 0)
            //this.dbContext.Orders.Remove(this.dbContext.Orders.FirstOrDefault(u=>u.Id==int.Parse(HttpContext.Session.GetString("current order"))));
            var order = new Order()
            {
                Description = "",
                Price = 0,
                Dishes = new List<Dish>()
            };
            this.dbContext.Orders.Add(order);
            try
            {
                this.dbContext.SaveChanges();
                HttpContext.Session.SetString("current order", this.dbContext.Orders.FirstOrDefault(u=>u==order).Id.ToString());
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            
            return RedirectToAction("GetAll","Dish");
        }

    }
}
