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
    public class DiningTableControllerTest
    {

        private DiningTableController diningTableController;
        private List<DiningTable> diningTableList = new List<DiningTable>();
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static DiningTableControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public DiningTableControllerTest()
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


            diningTableController = new DiningTableController(context);

            diningTableController.ControllerContext.HttpContext = httpContext;

            diningTableList = new List<DiningTable>()
            {
                new DiningTable()
                {
                    Busy=false,
                    Seats=1
                },
                new DiningTable()
                {
                    Busy=false,
                    Seats=2
                },
                new DiningTable()
                {
                    Busy=false,
                    Seats=3
                },
                new DiningTable()
                {
                    Busy=false,
                    Seats=4
                }
            };
        }


        [Fact]
        public async void GetDiningTableAll()
        {
            var result = await diningTableController.GetAll();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnDiningTable = resultView?.ViewData.Model;

            Assert.IsType<List<DiningTable>>(returnDiningTable);

        }


        [Theory]
        [InlineData(2)]
        public async void SearchDiningTable(int id)
        {
            var result = await diningTableController.SearchTable(id);

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnDiningTable = resultView?.ViewData.Model;

            Assert.IsType<List<DiningTable>>(returnDiningTable);

        }



        [Theory]
        [InlineData(1)]
        public async void DeleteDiningTable(int id)
        {

            var result = await diningTableController.Delete(id);

            Assert.IsType<RedirectToActionResult>(result);

        }



        [Theory]
        [InlineData(1)]
        public async void EditDiningTable(int id)
        {
            var editTable = new TableDto()
            {
                Seats=9
            };

            var result = await diningTableController.Edit(id, editTable);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await diningTableController.SearchTable(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetDiningTable = (List<DiningTable>?)resultGetView?.ViewData.Model;

            Assert.Equal(editTable.Seats, returnGetDiningTable.FirstOrDefault(x => x.Id == id).Seats);

        }


        [Fact]
        public async void CreateDiningTable()
        {
            var newTable = new TableDto()
            {
                Seats = 9
            };


            var resultGetAll = await diningTableController.GetAll();

            Assert.IsType<ViewResult>(resultGetAll);

            var resultView = Assert.IsType<ViewResult>(resultGetAll);

            var returnDiningTable = (List<DiningTable>?)resultView?.ViewData.Model;

            int id = returnDiningTable.Count - 1;

            var result = await diningTableController.Create(newTable);

            Assert.IsType<RedirectToActionResult>(result);

            var resultGet = await diningTableController.SearchTable(id);

            Assert.IsType<ViewResult>(resultGet);

            var resultGetView = Assert.IsType<ViewResult>(resultGet);

            var returnGetDiningTable = (List<DiningTable>?)resultGetView?.ViewData.Model;

            Assert.Equal(newTable.Seats, returnGetDiningTable.FirstOrDefault(x => x.Id == id).Seats);
        }
    }
}
