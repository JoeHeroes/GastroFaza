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
    public class ReservationControllerTest
    {

        private ReservationController reservationController;
        private List<Reservation> reservationList = new List<Reservation>();
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static ReservationControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public ReservationControllerTest()
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

            reservationController = new ReservationController(context);

            reservationController.ControllerContext.HttpContext = httpContext;

            reservationList = new List<Reservation>()
            {
               new Reservation()
               {
                   ClientId = 1,
                   TableIdContainer = "1",
                   TableId = new int[] { 1}
               },
             
            };
        }


        [Fact]
        public async void GetReservationAll()
        {
            var result = await reservationController.GetAll();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnReservation = resultView?.ViewData.Model;

            Assert.IsType<List<Reservation>>(returnReservation);

        }


        [Fact]
        public void CheckReservation()
        {
            var result = reservationController.Check();

            Assert.IsType<ViewResult>(result);

        }


        [Theory]
        [InlineData(1)]
        public async void DeleteReservation(int id)
        {

            var result = await reservationController.Delete(id);

            Assert.IsType<NotFoundResult>(result);

        }



        [Theory]
        [InlineData(1)]
        public async void EditReservation(int id)
        {

            var editReservation = new ReservationWorkerDto()
            {
                ClientId = 1,
                TableId = new[] { 1 },
                DateOfReservation = DateTime.Now,
                HourOfReservation = DateTime.Now.AddDays(-1)
            };

            var result = await reservationController.EditReservation(id, editReservation);

            Assert.IsType<NotFoundResult>(result);

        }


        [Fact]
        public async void CreateReservation()
        {
            var newReservation = new ReservationWorkerDto()
            {
                ClientId = 1,
                TableId = new[] { 1 },
                DateOfReservation = new DateTime(),
                HourOfReservation = new DateTime(),
            };


            var result = await reservationController.CreateReservation(newReservation);

            Assert.IsType<RedirectToActionResult>(result);

        }
    }
}
