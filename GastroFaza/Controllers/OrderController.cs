using GastroFaza.Models;
using GastroFaza.Models.DTO;
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
        
        [Route("ClientsOrders")]
        public IActionResult ClientsOrders()
        {
            var orders = this.dbContext.Orders.Where(o => o.Status == Status.Przygotowywanie);
            return View(orders);
        }
        [Route("OrderDetails")]
        public IActionResult OrderDetails(int OrderId)
        {
            var dishes = this.dbContext.Orders.Where(o => o.Id == OrderId).SelectMany(o => o.Dishes);
            HttpContext.Session.SetString("orderId", OrderId.ToString());
            return View(dishes);
        }
        [Route("OrderIsReady")]
        public IActionResult OrderIsReady(int OrderId)
        {
            Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Status = Status.Gotowe;
            dbContext.SaveChanges();
            return RedirectToAction("ClientsOrders", "Order");
        }
        
        [Route("Order")]
        public IActionResult Order()
        {
            if (HttpContext.Session.GetString("current order") == null || HttpContext.Session.GetString("current order") == String.Empty)
            {
                return RedirectToAction("Create");
            }
            int id = int.Parse(HttpContext.Session.GetString("current order"));

            var dishOrder = this.dbContext.DishOrders.Where(x => x.OrderId == id);

            List<Dish> orders = new List<Dish>();

            var dishList = this.dbContext.Dishs.ToList();

            foreach (var x in dishOrder)
            {
                var dish = dishList.FirstOrDefault(d => d.Id == x.DishesId);
                orders.Add(dish);
            }

            return View(orders);
        }
        [Route("PayForOrder")]
        public IActionResult PayForOrder()
        {
            int id = int.Parse(HttpContext.Session.GetString("current order"));
            Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if(order== null)
            {
                throw new Exception("Order not found");
            }
            order.Status= Status.Przygotowywanie;
            dbContext.SaveChanges();
            HttpContext.Session.SetString("current order", String.Empty);
            return View();
        }
        public IActionResult RemoveDishFromOrder(Dish dish)
        {

            int id = int.Parse(HttpContext.Session.GetString("current order"));

            var dishOrder = this.dbContext.DishOrders.SingleOrDefault( x => x.OrderId == id && x.DishesId == dish.Id);

            this.dbContext.DishOrders.Remove(dishOrder);
           
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return RedirectToAction("Order");
        }

        [Route("Create")]
        public IActionResult Create()
        {
            var order = new Order();
            var user = this.dbContext.Workers.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("email"));
            order.AddedById = user.Id;

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
        public IActionResult GetAllOrders()
        {
            IEnumerable<Order> orders = this.dbContext.Orders;

            return View(orders);
        }
        public IActionResult Edit(int Id)
        {
            var order = this.dbContext.Orders.Where(s => s.Id == Id).FirstOrDefault();

            return View(order);
        }
        [HttpPost]
        public IActionResult Edit(int? id, OrderDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Orders.Find(id);
                model.Status = modelDTO.Status;
                model.Description = modelDTO.Description;
                model.Price = modelDTO.Price;
                model.AddedById = modelDTO.AddedById;

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("GetAllOrders");
            }

            return View(modelDTO);
        }
        public IActionResult Delete(int? id)
        {
            var order = this.dbContext.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            this.dbContext.Orders.Remove(order);
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return RedirectToAction("GetAllOrders");
        }
    }
}
