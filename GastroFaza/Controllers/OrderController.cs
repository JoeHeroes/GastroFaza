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
            if (HttpContext.Session.GetString("Role") == "Cook")
            {
                var orders = this.dbContext.Orders.Where(o => o.Status == Status.Przyjete || o.Status == Status.Przygotowywanie);
                return View(orders);
            }
            else if (HttpContext.Session.GetString("Role") == "Waiter")
            {
                var orders = this.dbContext.Orders.Where(o => o.Status == Status.Przyjete || o.Status == Status.Gotowe || o.Status == Status.Dostarczone || o.Status == Status.Odebrane);
                return View(orders);
            }
            return View();
        }
        [Route("OrderDetails")]
        public IActionResult OrderDetails(int orderId)
        {
            HttpContext.Session.Remove("OrderStatus"); // czyści zawartość sesji by wstawić nowe dane do szczegółów zamówienia
            HttpContext.Session.Remove("orderId");

            var order = this.dbContext.Orders.FirstOrDefault(x => x.Id == orderId); //status potrzebny w sesji -> patrz OrderDetails linia 51
            if (order.Status == Status.Przygotowywanie)
                HttpContext.Session.SetString("OrderStatus", "Preparing");

            HttpContext.Session.SetString("orderId", orderId.ToString());
            var dishOrder = this.dbContext.DishOrders.Where(x => x.OrderId == orderId);

            List<Dish> dishes = new List<Dish>();

            var dishList = this.dbContext.Dishs.ToList();

            foreach (var x in dishOrder)
            {
                var dish = dishList.FirstOrDefault(d => d.Id == x.DishesId);
                dishes.Add(dish);
            }
            
            User user = this.dbContext.Clients.FirstOrDefault(x => x.Id == order.AddedById);
            if (user == null)
                user = this.dbContext.Workers.FirstOrDefault(x => x.Id == order.AddedById);
            OrderDetailsDto orderDetails = new OrderDetailsDto
            {
                ClientId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                OrderId = orderId,
                Dishes = dishes,
                Status = order.Status,
                Description = order.Description,
                Price = order.Price,
            };

            return View(orderDetails);
        }

        [Route("OrderIsReceived")]
        public IActionResult OrderIsReceived(int OrderId)
        {
            Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Status = Status.Odebrane;
            dbContext.SaveChanges();
            return RedirectToAction("ClientsOrders", "Order");
        }

        [Route("OrderIsInProgress")]
        public IActionResult OrderIsInProgress(int OrderId)
        {
            Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Status = Status.Przygotowywanie;
            dbContext.SaveChanges();
            return RedirectToAction("ClientsOrders", "Order");
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

        [Route("OrderIsTaken")]
        public IActionResult OrderIsTaken(int OrderId)
        {
            Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Status = Status.Odebrane;
            dbContext.SaveChanges();
            return RedirectToAction("ClientsOrders", "Order");
        }

        [Route("OrderIsDelivered")]
        public IActionResult OrderIsDelivered(int OrderId)
        {
            Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Status = Status.Dostarczone;
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
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Status = Status.Przyjete;
            dbContext.SaveChanges();
            HttpContext.Session.SetString("current order", String.Empty);
            return View();
        }
        public IActionResult RemoveDishFromOrder(Dish dish)
        {

            int id = int.Parse(HttpContext.Session.GetString("current order"));

            var dishOrder = this.dbContext.DishOrders.SingleOrDefault(x => x.OrderId == id && x.DishesId == dish.Id);

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

            
            if (this.dbContext.Workers.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("email")) != null)
            {
                var user = this.dbContext.Workers.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("email"));
                order.AddedById = user.Id;
            }
            else if(this.dbContext.Clients.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("email")) != null)
            {
                var user = this.dbContext.Clients.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("email"));
                order.AddedById = user.Id;
            } else
            {
                return RedirectToAction("Login", "Account");
            }

            order.Status = Status.Nowe;

            this.dbContext.Orders.Add(order);
            try
            {
                this.dbContext.SaveChanges();
                HttpContext.Session.SetString("current order", this.dbContext.Orders.FirstOrDefault(u => u == order).Id.ToString());
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return RedirectToAction("GetAll", "Dish");
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