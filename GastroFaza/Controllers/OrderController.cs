using GastroFaza.Models;
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
            Order order = this.dbContext.Orders.FirstOrDefault();
            //if(order.Dishes.Count == 0)
            //    return RedirectToAction("GetAll", "Dish");
            HttpContext.Session.SetString("current order", order.Id.ToString());
            return View(order);
        }
        public IActionResult RemoveDishFromOrder(Order order, Dish dish)
        {
            order.Dishes.Remove(dish);
            this.dbContext.Orders.FirstOrDefault(u=>u.Id==order.Id).Dishes.Remove(dish);
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
