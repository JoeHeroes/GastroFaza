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
    public class WorkerControllerTest
    {
        private WorkerController workerController;
        List<Worker> workerList = new List<Worker>();
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static WorkerControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public WorkerControllerTest()
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


            workerController = new WorkerController(context);

            workerController.ControllerContext.HttpContext = httpContext;


            workerList = new List<Worker>()
            {
                new Worker()
                {
                     Email = "Test@wp.pl",
                     FirstName = "Test",
                     LastName = "Test",
                     DateOfBirth = new DateTime(),
                     Nationality = "Test",
                     RoleId = 3,
                },
                new Worker()
                {
                     Email = "Test2@wp.pl",
                     FirstName = "Tes2",
                     LastName = "Test2",
                     DateOfBirth = new DateTime(),
                     Nationality = "Test2",
                     RoleId = 3,
                }
            };
        }


        [Fact]
        public async void GetWorkerAll()
        {
            var result = await workerController.GetAll();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnWorker = resultView?.ViewData.Model;

            Assert.IsType<List<WorkerDTO>>(returnWorker);

        }


        [Theory]
        [InlineData(1)]
        public async void GetWorker(int id)
        {
            var result = await workerController.GetOne(id);

            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData(3)]
        public async void DeleteWorker(int id)
        {
            var result = await workerController.Delete(id);

            Assert.IsType<NotFoundResult>(result);


            var resultGet = await workerController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetWorker = (Worker?)resultGetView?.ViewData.Model;

            Assert.Equal(null, returnGetWorker);

        }



        [Theory]
        [InlineData(4)]
        public async void EditWorker(int id)
        {

            var editWorker = new UpdateWorkerDto()
            {
                Email = "Test2@wp.pl",
                FirstName = "Tes2",
                LastName = "Test2",
                DateOfBirth = new DateTime(),
                Nationality = "Test2",
                Salary = 5000,
                Rating = 5,
                RoleId= 3
            };

            var result = await workerController.Edit(id, editWorker);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await workerController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetWorker = (Worker?)resultGetView?.ViewData.Model;

            Assert.Equal(editWorker.Email, returnGetWorker.Email);
            Assert.Equal(editWorker.Salary, returnGetWorker.Salary);
            Assert.Equal(editWorker.RoleId, returnGetWorker.RoleId);
        }
        


        [Theory]
        [InlineData(2)]
        public async void RateWorker(int id)
        {

            var result = workerController.RateWorker(id);

            Assert.IsType<ViewResult>(result);

            var resultGet = await workerController.GetOne(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetWorker = (Worker?)resultGetView?.ViewData.Model;

            Assert.Equal(null, returnGetWorker);

        }
    }
}