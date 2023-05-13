using GastroFaza.Controllers;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GastroFaza.Test
{
    public class ClientControllerTest
    {
        private ClientController clientController;
        List<Client> clientList = new List<Client>();
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static ClientControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public ClientControllerTest()
        {
            var context = new RestaurantDbContext(dbContextOptions);
            DataTestDBInitializer db = new DataTestDBInitializer();
            db.Seed(context);

            clientController = new ClientController(context);
            clientList = new List<Client>()
            {
                new Client()
                {
                     Id = 1,
                     Email = "test1@wp.pl",
                     FirstName = "test1",
                     LastName = "test1",
                     DateOfBirth = new DateTime(),
                     Nationality = "Poland",
                },
                new Client()
                {
                     Id = 2,
                     Email = "test2@wp.pl",
                     FirstName = "test2",
                     LastName = "test2",
                     DateOfBirth = new DateTime(),
                     Nationality = "Poland",
                },
                new Client()
                {
                     Id = 3,
                     Email = "test3@wp.pl",
                     FirstName = "test3",
                     LastName = "test3",
                     DateOfBirth = new DateTime(),
                     Nationality = "Poland",
                }
            };
        }


        [Fact]
        public async void GetClientAll()
        {
            var result = await clientController.GetAll();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnClient= resultView?.ViewData.Model;

            Assert.IsType<List<Client>>(returnClient);

        }


        [Theory]
        [InlineData(2)]
        public async void GetClient(int id)
        {
            var result = await clientController.GetOne(id);

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnClient = (Client?)resultView?.ViewData.Model;

            Assert.Equal(id, returnClient.Id);

        }



        [Theory]
        [InlineData(2)]
        public async void DeleteClient(int id)
        {

            var result = await clientController.Delete(id);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await clientController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetClient = (Client?)resultGetView?.ViewData.Model;

            Assert.Equal(null, returnGetClient);

        }



        [Theory]
        [InlineData(2)]
        public async void EditClient(int id)
        {

            var editClient = new UpdateClientDto()
            {
                Email = "test3@wp.pl",
            };

            var result = await clientController.Edit(id, editClient);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await clientController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetClient = (Client?)resultGetView?.ViewData.Model;

            Assert.Equal(editClient.Email, returnGetClient.Email);
        }


        [Fact]
        public async void CreateClient()
        {
            var newClient = new CreateClientDto()
            {
                Email = "test3@wp.pl",
                FirstName = "test3",
                LastName = "test3",
                DateOfBirth = new DateTime(),
                Nationality = "Poland",
            };


            var result = await clientController.Create(newClient);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGetAll = await clientController.GetAll();

            Assert.IsType<ViewResult>(resultGetAll);

            var resultView = Assert.IsType<ViewResult>(resultGetAll);

            var returnClient = (List<Client>?)resultView?.ViewData.Model;

            var resultGet = await clientController.GetOne(returnClient.Count - 1);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetClient = (Client?)resultGetView?.ViewData.Model;

            Assert.Equal(newClient.Email, returnGetClient.Email);
            Assert.Equal(newClient.FirstName, returnGetClient.FirstName);
            Assert.Equal(newClient.LastName, returnGetClient.LastName);
            Assert.Equal(newClient.DateOfBirth, returnGetClient.DateOfBirth);
            Assert.Equal(newClient.Nationality, returnGetClient.Nationality);
        }
    }
}
