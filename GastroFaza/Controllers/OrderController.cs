using GastroFaza.Models;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            if(HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true")
                {
                    HttpContext.Session.Remove("OrderStatus"); // czyści zawartość sesji by wstawić nowe dane do szczegółów zamówienia
                    HttpContext.Session.Remove("orderId");

                    var order = await this.dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId); //status potrzebny w sesji -> patrz OrderDetails linia 51
                    if (order.Status == Status.Przygotowywanie)
                        HttpContext.Session.SetString("OrderStatus", "Preparing");

                    HttpContext.Session.SetString("orderId", orderId.ToString());
                    var dishOrder = this.dbContext.DishOrders.Where(x => x.OrderId == orderId);

                    List<Dish> dishes = new List<Dish>();

                    var dishList = await this.dbContext.Dishs.ToListAsync();

                    foreach (var x in dishOrder)
                    {
                        var dish = dishList.FirstOrDefault(d => d.Id == x.DishesId);
                        dishes.Add(dish);
                    }

                    Client client = await this.dbContext.Clients.FirstOrDefaultAsync(x => x.Id == order.AddedById);


                    if (client == null)
                    {
                        Worker worker = await this.dbContext.Workers.FirstOrDefaultAsync(x => x.Id == order.AddedById);
                        OrderDetailsDto orderDetailsWorker = new OrderDetailsDto
                        {
                            ClientId = worker.Id,
                            Email = worker.Email,
                            FirstName = worker.FirstName,
                            LastName = worker.LastName,
                            OrderId = orderId,
                            Dishes = dishes,
                            Status = order.Status,
                            Description = order.Description,
                            Price = order.Price,
                        };
                        return View(orderDetailsWorker);
                    }

                    OrderDetailsDto orderDetails = new OrderDetailsDto
                    {
                        ClientId = client.Id,
                        Email = client.Email,
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        PhoneNumber = client.PhoneNumber,
                        OrderId = orderId,
                        Dishes = dishes,
                        Status = order.Status,
                        Description = order.Description,
                        Price = order.Price,
                    };

                    return View(orderDetails);
                }
                return Forbid();
            }
            return Forbid();
        }

        [Route("OrderIsReceived")]
        public async Task<IActionResult> OrderIsReceived(int OrderId)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Cook")
                {
                    Order order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
                    if (order == null)
                    {
                        throw new Exception("Order not found");
                    }
                    order.Status = Status.Odebrane;
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("ClientsOrders", "Order");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        [Route("OrderIsInProgress")]
        public async Task<IActionResult> OrderIsInProgress(int OrderId)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Cook")
                {
                    Order order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
                    if (order == null)
                    {
                        throw new Exception("Order not found");
                    }
                    order.Status = Status.Przygotowywanie;
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("ClientsOrders", "Order");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        [Route("OrderIsReady")]
        public async Task<IActionResult> OrderIsReady(int OrderId)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Cook")
                {
                    Order order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
                    if (order == null)
                    {
                        throw new Exception("Order not found");
                    }
                    order.Status = Status.Gotowe;
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("ClientsOrders", "Order");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        [Route("OrderIsTaken")]
        public async Task<IActionResult> OrderIsTaken(int OrderId)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Waiter")
                {
                    Order order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
                    if (order == null)
                    {
                        throw new Exception("Order not found");
                    }
                    order.Status = Status.Odebrane;
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("ClientsOrders", "Order");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
    }

        [Route("OrderIsDelivered")]
        public async Task<IActionResult> OrderIsDelivered(int OrderId)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Waiter")
                {
                    Order order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
                    if (order == null)
                    {
                        throw new Exception("Order not found");
                    }
                    order.Status = Status.Dostarczone;
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("ClientsOrders", "Order");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
    }

        [Route("Order")]
        public async Task<IActionResult> Order()
        {
            if (HttpContext.Session.GetString("current order") == null || HttpContext.Session.GetString("current order") == String.Empty)
            {
                return RedirectToAction("Create");
            }
            int id = int.Parse(HttpContext.Session.GetString("current order"));

            var dishOrder = this.dbContext.DishOrders.Where(x => x.OrderId == id);

            List<Dish> orders = new List<Dish>();

            var dishList = await this.dbContext.Dishs.ToListAsync();

            foreach (var x in dishOrder)
            {
                var dish = dishList.FirstOrDefault(d => d.Id == x.DishesId);
                orders.Add(dish);
            }
            return View(orders);
        }


        [Route("ChooseOption")]
        public IActionResult ChooseOption()
        {
            return View();
        }



        [Route("Delivery")]
        public IActionResult Delivery()
        {
            return View();
        }


        [HttpPost]
        [Route("Delivery")]
        public async Task<IActionResult> Delivery(AddressDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var newAddress = new Address()
                {
                    City = modelDTO.City,
                    Street = modelDTO.Street,
                    PostalCode = modelDTO.PostalCode,
                };
                
                this.dbContext.Addresses.Add(newAddress);

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                int idOrder = int.Parse(HttpContext.Session.GetString("current order"));
                Order order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == idOrder);
                Address address = await this.dbContext.Addresses.FirstOrDefaultAsync(x => x.Street == newAddress.Street && x.City == newAddress.City);
                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                order.Delivery = true;
                order.AddresId = address.AddressId;

                return RedirectToAction("PayForOrder");
            }

            return View(modelDTO);
        }


        [Route("PayForOrder")]
        public async Task<IActionResult> PayForOrder()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "false")
                {
                    int id = int.Parse(HttpContext.Session.GetString("current order"));
                    Order order =  await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
                    if (order == null)
                    {
                        throw new Exception("Order not found");
                    }
                    order.Status = Status.Przyjete;

                    string userEmail = HttpContext.Session.GetString("email");
                    var client = await this.dbContext.Clients.FirstOrDefaultAsync(u => u.Email == userEmail);

                    var dishOrder = this.dbContext.DishOrders.Where(x => x.OrderId == id);
                    var dishList = await this.dbContext.Dishs.ToListAsync();

                    History historyOrder = new History();
                    historyOrder.Date = DateTime.Now;
                    historyOrder.Stars = 0;

                    foreach (var x in dishOrder)
                    {
                        var dish = dishList.FirstOrDefault(d => d.Id == x.DishesId);
                        historyOrder.Dishes += dish.Name.ToString() + ", ";
                    }

                    historyOrder.AddedById = client.Id;

                    this.dbContext.Histories.Add(historyOrder);

                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    HttpContext.Session.SetString("current order", String.Empty);
                    return View();
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }


        public async Task<IActionResult> RemoveDishFromOrder(Dish dish)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") != "Cook")
                {

                    int id = int.Parse(HttpContext.Session.GetString("current order"));

                    var dishOrder = await this.dbContext.DishOrders.SingleOrDefaultAsync(x => x.OrderId == id && x.DishesId == dish.Id);

                    this.dbContext.DishOrders.Remove(dishOrder);

                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }

                    return RedirectToAction("Order");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") != "Cook")
                {
                    var order = new Order();
            
                    if (await this.dbContext.Workers.FirstOrDefaultAsync(u => u.Email == HttpContext.Session.GetString("email")) != null)
                    {
                        var user = await this.dbContext.Workers.FirstOrDefaultAsync(u => u.Email == HttpContext.Session.GetString("email"));
                        order.AddedById = user.Id;
                    }
                    else if(await this.dbContext.Clients.FirstOrDefaultAsync(u => u.Email == HttpContext.Session.GetString("email")) != null)
                    {
                        var user = await this.dbContext.Clients.FirstOrDefaultAsync(u => u.Email == HttpContext.Session.GetString("email"));
                        order.AddedById = user.Id;
                    } else
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    order.Status = Status.Nowe;

                    this.dbContext.Orders.Add(order);
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                        HttpContext.Session.SetString("current order",this.dbContext.Orders.FirstOrDefault(u => u == order).Id.ToString());
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("GetAll", "Dish");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
}
        public async Task<IActionResult> GetAllOrders()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true")
                {
                    var orders = await  this.dbContext.Orders.ToListAsync();

                    return View(orders);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
                {
                    var order = await this.dbContext.Orders.FirstOrDefaultAsync(s => s.Id == Id);

                    return View(order);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int? id, OrderDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = await this.dbContext.Orders.FindAsync(id);
                model.Status = modelDTO.Status;
                model.Description = modelDTO.Description;
                model.Price = modelDTO.Price;
                model.AddedById = modelDTO.AddedById;

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("GetAllOrders");
            }

            return View(modelDTO);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
                {
                    var order = await this.dbContext.Orders.FindAsync(id);
                    if (order == null)
                    {
                        return NotFound();
                    }

                    this.dbContext.Orders.Remove(order);
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("GetAllOrders");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }



        [Route("History")]
        public async Task<IActionResult> History()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                var histories = await this.dbContext.Histories.ToListAsync();

                if (HttpContext.Session.GetString("isWorker") != "true")
                {
                    string userEmail = HttpContext.Session.GetString("email");
                    var client = await this.dbContext.Clients.FirstOrDefaultAsync(u => u.Email == userEmail);
                    IEnumerable<History> clientHistories = histories.Where(r => r.AddedById == client.Id);
                    return View(clientHistories);
                }
                else
                {
                    return View(histories);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> ClearHistory()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                string userEmail = HttpContext.Session.GetString("email");
                var client =  await this.dbContext.Clients.FirstOrDefaultAsync(u => u.Email == userEmail);

                var clientHist = this.dbContext.Histories.Where(x => x.AddedById == client.Id);

                foreach (var del in clientHist)
                {
                    this.dbContext.Histories.Remove(del);
                }

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                return RedirectToAction("History");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public IActionResult RateOrder(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") != "true")
                {
                    return View();
                }
                return Forbid();            
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RateOrder(int? id, int newStars)
        {
            if (ModelState.IsValid)
            {
                var model = await this.dbContext.Histories.FindAsync(id);
                model.Stars = newStars;
               try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
            }
            return RedirectToAction("History");
        }
    }
}