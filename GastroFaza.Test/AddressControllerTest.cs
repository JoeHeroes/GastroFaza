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
    public class AddressControllerTest
    {

        private AddressController addressController;
        private List<Address> addressList = new List<Address>();
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static AddressControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public AddressControllerTest()
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


            addressController = new AddressController(context);

            addressController.ControllerContext.HttpContext = httpContext;

            addressList = new List<Address>()
            {
                new Address()
                {
                    City = "test",
                    Street = "test",
                    PostalCode = "test",
                }
            };
        }


        [Fact]
        public async void GetAddressAll()
        {
            var result = await addressController.GetAll();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnAddress = resultView?.ViewData.Model;

            Assert.IsType<List<Address>>(returnAddress);

        }


        [Theory]
        [InlineData(2)]
        public async void GetAddress(int id)
        {
            var result = await addressController.GetOne(id);

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnAddress = (Address?)resultView?.ViewData.Model;

            Assert.Equal(id, returnAddress.AddressId);

        }



        [Theory]
        [InlineData(1)]
        public async void DeleteClient(int id)
        {

            var result = await addressController.Delete(id);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await addressController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetAddress = (Address?)resultGetView?.ViewData.Model;

            Assert.Equal(null, returnGetAddress);

        }



        [Theory]
        [InlineData(1)]
        public async void EditClient(int id)
        {

            var editAddress = new AddressDto()
            {
                City= "testCity",
                Street= "testStreet",
                PostalCode= "testPostalCode"
            };

            var result = await addressController.Edit(id, editAddress);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await addressController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetAddress = (Address?)resultGetView?.ViewData.Model;

            Assert.Equal(editAddress.City, returnGetAddress.City);
            Assert.Equal(editAddress.Street, returnGetAddress.Street);
            Assert.Equal(editAddress.PostalCode, returnGetAddress.PostalCode);

        }


        [Fact]
        public async void CreateAddress()
        {
            var newAddress = new AddressDto()
            {
                City = "3",
                Street = "3",
                PostalCode = "3",
            };


            var result = await addressController.Create(newAddress);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGetAll = await addressController.GetAll();

            Assert.IsType<ViewResult>(resultGetAll);

            var resultView = Assert.IsType<ViewResult>(resultGetAll);

            var returnAddress = (List<Address>?)resultView?.ViewData.Model;

            var resultGet = await addressController.GetOne(returnAddress.Count()-1);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetAddress = (Address?)resultGetView?.ViewData.Model;

            Assert.Equal(newAddress.City, returnGetAddress.City);
            Assert.Equal(newAddress.Street, returnGetAddress.Street);
            Assert.Equal(newAddress.PostalCode, returnGetAddress.PostalCode);
        }
    }
}
