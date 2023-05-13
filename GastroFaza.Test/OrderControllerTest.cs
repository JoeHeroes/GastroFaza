using GastroFaza.Controllers;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;


namespace GastroFaza.Test
{
    public class OrderControllerTest
    {

        private OrderController orderController;
        private List<Order> orderList = new List<Order>();
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static OrderControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public OrderControllerTest()
        {
            var context = new RestaurantDbContext(dbContextOptions);
            DataTestDBInitializer db = new DataTestDBInitializer();

            db.Seed(context);

            SessionTestInitializer session = new SessionTestInitializer();

            session.SetString("id", "1");
            session.SetString("email", "Test@wp.pl");
            session.SetString("isWorker", "true");
            session.SetString("Role", "Waiter");
            session.SetString("current order", "1");

            var httpContext = new DefaultHttpContext();

            httpContext.Features.Set<ISessionFeature>(new SessionFeature { Session = session });

            orderController = new OrderController(context);

            orderController.ControllerContext.HttpContext = httpContext;

            orderList = new List<Order>()
            {
               new Order()
               {
                   Id = 1,
                   Status = Status.Nowe,
                   Description = "Test",
                   Price = 1.1,
                   AddedById = 1
               }
            };
        }


        [Fact]
        public async void GetOrderAll()
        {
            var result = await orderController.GetAllOrders();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnOrder = resultView?.ViewData.Model;

            Assert.IsType<List<Order>>(returnOrder);

        }


        [Theory]
        [InlineData(2)]
        public async void GetOrder(int id)
        {
            var result = await orderController.OrderDetails(id);

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnOrder = (Order?)resultView?.ViewData.Model;

            Assert.Equal(id, returnOrder.Id);

        }

        [Theory]
        [InlineData(1)]
        public async void OrderIsReceived(int id)
        {
            var result = await orderController.OrderIsReceived(id);

            Assert.IsType<ForbidResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async void OrderIsInProgress(int id)
        {
            var result = await orderController.OrderIsInProgress(id);

            Assert.IsType<ForbidResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async void OrderIsReady(int id)
        {
            var result = await orderController.OrderIsReady(id);

            Assert.IsType<ForbidResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async void OrderIsTaken(int id)
        {
            var result = await orderController.OrderIsTaken(id);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async void OrderIsDelivered(int id)
        {
            var result = await orderController.OrderIsDelivered(id);

            Assert.IsType<RedirectToActionResult>(result);
        }



        [Theory]
        [InlineData(1)]
        public async void DeleteOrder(int id)
        {

            var result = await orderController.Delete(id);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await orderController.OrderDetails(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetOrder = (Order?)resultGetView?.ViewData.Model;

            Assert.Equal(null, returnGetOrder);

        }

        [Fact]
        public async void OrderGetListDishs()
        {
            var result = await orderController.Order();
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void DeliveryOrder()
        {
            var result = orderController.Delivery();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void PayForOrder()
        {
            var result = await orderController.PayForOrder();
            Assert.IsType<ForbidResult>(result);
        }


        [Fact]
        public async void RemoveDishFromOrder()
        {

            var removeDish = new Dish()
            {
                Id= 1,
            };


            var result = await orderController.RemoveDishFromOrder(removeDish);
            Assert.IsType<RedirectToActionResult>(result);
        }


        [Fact]
        public async void HistoryOrder()
        {
            var result = await orderController.History();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void ClearHistoryOrder()
        {
            var result = await orderController.ClearHistory();
            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async void RateOrder(int id)
        {
            var result = orderController.RateOrder(id);
            Assert.IsType<ForbidResult>(result);
        }


        [Theory]
        [InlineData(1)]
        public async void EditOrder(int id)
        {
            var editOrder = new OrderDto()
            {
                Status = Status.Przyjete,
                Description = "EditTest",
                Price = 15.5,
            };

            var result = await orderController.Edit(id, editOrder);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await orderController.OrderDetails(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetOrder = (Order?)resultGetView?.ViewData.Model;

            Assert.Equal(editOrder.Status, returnGetOrder.Status);
            Assert.Equal(editOrder.Description, returnGetOrder.Description);
            Assert.Equal(editOrder.Price, returnGetOrder.Price);


        }


        [Fact]
        public async void CreateOrder()
        {

            var result = await orderController.Create();

            Assert.IsType<RedirectToActionResult>(result);

        }
    }
}
