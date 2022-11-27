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
            IEnumerable<Order> orders = this.dbContext.Orders;
            return View(orders);
        }

        [Route("Create")]
        public IActionResult Create()
        {
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
