using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class OrderController : Controller
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IWebHostEnvironment webHost;

        public OrderController(RestaurantDbContext dbContext, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }
       
        [Route("GetAll")]
        public IActionResult GetAll()
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
