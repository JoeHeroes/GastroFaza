using GastroFaza.Controllers;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;


namespace GastroFaza.Test
{
    public class RestaurantControllerTest
    {
        private RestaurantController restaurantController;
        List<Restaurant> restaurantList = new List<Restaurant>();
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static RestaurantControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public RestaurantControllerTest()
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




            restaurantController = new RestaurantController(context);

            restaurantController.ControllerContext.HttpContext = httpContext;


            restaurantList = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "Test",
                    Description="TestTestTest",
                    HasDelivery= true,
                    ContactEmail = "Test@wp.pl",
                    ContactNumber= "123 456 789",
                    Address= new Address()
                    {
                        City = "Test",
                        PostalCode="12-234",
                        Street ="Test"
                    },
                },
                new Restaurant()
                {
                    Name = "Test2",
                    Description="TestTestTest2",
                    HasDelivery= true,
                    ContactEmail = "Test2@wp.pl",
                    ContactNumber= "123 456 789",
                    Address= new Address()
                    {
                        City = "Test2",
                        PostalCode="12-234",
                        Street ="Test2"
                    },
                },
            };
        }


        [Fact]
        public async void GetRestaurantAll()
        {
            var result = await restaurantController.GetAll();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnRestaurant = resultView?.ViewData.Model;

            Assert.IsType<List<Restaurant>>(returnRestaurant);

        }


        [Theory]
        [InlineData(1)]
        public async void GetRestaurant(int id)
        {
            var result = await restaurantController.GetOne(id);

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnRestaurant = (Restaurant?)resultView?.ViewData.Model;

            Assert.Equal(id, returnRestaurant.Id);

        }

        [Theory]
        [InlineData(2)]
        public async void DeleteRestaurant(int id)
        {

            var result = await restaurantController.Delete(id);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await restaurantController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetRestaurant = (Restaurant?)resultGetView?.ViewData.Model;

            Assert.Equal(null, returnGetRestaurant);

        }



        [Theory]
        [InlineData(2)]
        public async void EditRestaurant(int id)
        {

            var editRestaurant = new UpdateRestaurantDto()
            {
                Name = "TestEdit",
                Description = "TestTestTestEdit",
                HasDelivery = true,
                ContactEmail = "TestEdit@wp.pl",
                ContactNumber = "123 456 780",
            };

            var result = await restaurantController.Edit(id, editRestaurant);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await restaurantController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetRestaurant = (Restaurant?)resultGetView?.ViewData.Model;

            Assert.Equal(editRestaurant.Name, returnGetRestaurant.Name);
            Assert.Equal(editRestaurant.Description, returnGetRestaurant.Description);
            Assert.Equal(editRestaurant.HasDelivery, returnGetRestaurant.HasDelivery);
            Assert.Equal(editRestaurant.ContactEmail, returnGetRestaurant.ContactEmail);
            Assert.Equal(editRestaurant.ContactNumber, returnGetRestaurant.ContactNumber);
        }


        [Fact]
        public async void CreateRestaurant()
        {
            var newRestaurant = new CreateRestaurantDto()
            {
                Name = "TestNew",
                Description = "TestTestTestNew",
                HasDelivery = true,
                ContactEmail = "TestNew@wp.pl",
                ContactNumber = "123 456 780",
                City = "TestNew",
                PostalCode = "12-230",
                Street = "TestNew"
            };


            var result = await restaurantController.Create(newRestaurant);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGetAll = await restaurantController.GetAll();

            Assert.IsType<ViewResult>(resultGetAll);

            var resultView = Assert.IsType<ViewResult>(resultGetAll);

            var returnRestaurant = (List<Restaurant>?)resultView?.ViewData.Model;

            var resultGet = await restaurantController.GetOne(returnRestaurant.Count);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetRestaurant = (Restaurant?)resultGetView?.ViewData.Model;

            Assert.Equal(newRestaurant.Name, returnGetRestaurant.Name);
            Assert.Equal(newRestaurant.Description, returnGetRestaurant.Description);
            Assert.Equal(newRestaurant.HasDelivery, returnGetRestaurant.HasDelivery);
            Assert.Equal(newRestaurant.ContactEmail, returnGetRestaurant.ContactEmail);
            Assert.Equal(newRestaurant.ContactNumber, returnGetRestaurant.ContactNumber);
        }
    }
}