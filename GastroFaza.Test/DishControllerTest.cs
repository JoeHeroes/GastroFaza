using GastroFaza.Controllers;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Models.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net.Http;


namespace GastroFaza.Test
{
    public class DishControllerTest
    {

        private DishController dishController;
        private List<Dish> dishList = new List<Dish>();
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static DishControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public DishControllerTest()
        {
            var context = new RestaurantDbContext(dbContextOptions);
            DataTestDBInitializer db = new DataTestDBInitializer();

            db.Seed(context);

            

            SessionTestInitializer session = new SessionTestInitializer();

            session.SetString("id", "1");
            session.SetString("email", "Test@wp.pl");
            session.SetString("isWorker", "true");
            session.SetString("Role", "Manager");

            var httpContext = new DefaultHttpContext();

            httpContext.Features.Set<ISessionFeature>(new SessionFeature { Session = session });

            var webHost = new Mock<IWebHostEnvironment>().Object;

            dishController = new DishController(context, webHost);

            dishController.ControllerContext.HttpContext = httpContext;

            dishList = new List<Dish>()
            {
                 new Dish()
               {
                   Name = "Test1",
                   Description = "Test1",
                   Price = 15.5,
                   DishType = DishType.Pizza,
                   ProfileImg = "Test/Test1.png"
               },
               new Dish()
               {
                   Name = "Test2",
                   Description = "Test2",
                   Price = 15.5,
                   DishType = DishType.Pizza,
                   ProfileImg = "Test/Test2.png"
               },
               new Dish()
               {
                   Name = "Test3",
                   Description = "Test3",
                   Price = 15.5,
                   DishType = DishType.Pizza,
                   ProfileImg = "Test/Test3.png"
               },
            };
        }


        [Fact]
        public async void GetDishAll()
        {
            var result = await dishController.GetAll();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnDish = resultView?.ViewData.Model;

            Assert.IsType<List<Dish>>(returnDish);

        }


        [Theory]
        [InlineData(2)]
        public async void GetDish(int id)
        {
            var result = await dishController.GetOne(id);

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnDish = (Dish?)resultView?.ViewData.Model;

            Assert.Equal(id, returnDish.Id);

        }



        [Theory]
        [InlineData(1)]
        public async void DeleteDish(int id)
        {

            var result = await dishController.Delete(id);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await dishController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetDish = (Dish?)resultGetView?.ViewData.Model;

            Assert.Equal(null, returnGetDish);

        }



        [Theory]
        [InlineData(1)]
        public async void EditDish(int id)
        {

            var editDish = new DishDto()
            {
                Name = "EditTest",
                Description = "EditTest",
                Price = 15.5,
                DishType = DishType.Pizza,
            };

            var result = await dishController.Edit(id, editDish);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await dishController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetDish = (Dish?)resultGetView?.ViewData.Model;

            Assert.Equal(editDish.Name, returnGetDish.Name);
            Assert.Equal(editDish.Description, returnGetDish.Description);
            Assert.Equal(editDish.DishType, returnGetDish.DishType);


        }


        [Fact]
        public async void CreateDish()
        {
            var newDish = new DishDto()
            {
                Name = "CreateTest",
                Description = "CreateTest",
                Price = 15.5,
                DishType = DishType.Pizza,
            };


            var result = await dishController.Create(newDish);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGetAll = await dishController.GetAll();

            Assert.IsType<ViewResult>(resultGetAll);

            var resultView = Assert.IsType<ViewResult>(resultGetAll);

            var returnDish = (List<Dish>?)resultView?.ViewData.Model;

            var resultGet = await dishController.GetOne(returnDish.Count);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetDish = (Dish?)resultGetView?.ViewData.Model;


            Assert.Equal(newDish.Name, returnGetDish.Name);
            Assert.Equal(newDish.Description, returnGetDish.Description);
            Assert.Equal(newDish.DishType, returnGetDish.DishType);
        }
    }
}
