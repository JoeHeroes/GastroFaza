using GastroFaza.Controllers;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GastroFaza.Test
{
    public class AccountControllerTest
    {
        private AccountController accountController;
        public static DbContextOptions<RestaurantDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=GastroFazaDB; Trusted_Connection=True";

        static AccountControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public AccountControllerTest()
        {

            var context = new RestaurantDbContext(dbContextOptions);
            DataTestDBInitializer db = new DataTestDBInitializer();

            db.Seed(context);


            SessionTestInitializer session = new SessionTestInitializer();

            session.SetString("id", "1");
            session.SetString("email", "Test@wp.pl");
            session.SetString("isAccount", "true");
            session.SetString("isWorker", "false");
            session.SetString("Role", "Manager");
            

            var httpContext = new DefaultHttpContext();

            httpContext.Features.Set<ISessionFeature>(new SessionFeature { Session = session });

            var webHost = new Mock<IWebHostEnvironment>().Object;
            var passwordClientHasher = new Mock<IPasswordHasher<Client>>();
            var passwordWorkerHasher = new Mock<IPasswordHasher<Worker>>();


            accountController = new AccountController(context, passwordClientHasher.Object, passwordWorkerHasher.Object, new AuthenticationSettings() , webHost);

            accountController.ControllerContext.HttpContext = httpContext;
        }


        [Fact]
        public async void LoginAccountShow()
        {
            var result = accountController.Login();

            Assert.IsType<RedirectToActionResult>(result);

        }

        [Fact]
        public async void LoginAccount()
        {
            var result = accountController.Login();
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void RegisterAccount()
        {
            var result = accountController.Register();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void CreateWorkerAccountAccount()
        {
            var result = await accountController.CreateWorkerAccount();

            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public async void LogoutAccount()
        {
            var result = accountController.Logout();

            Assert.IsType<RedirectToActionResult>(result);

        }



        [Fact]
        public async void WelcomeAccount()
        {
            var result = accountController.Welcome();

            Assert.IsType<ViewResult>(result);

        }




        [Fact]
        public async void ProfileAccount()
        {
            var result = await accountController.Profile();

            Assert.IsType<ViewResult>(result);

            var resultView = Assert.IsType<ViewResult>(result);

            var returnClient= resultView?.ViewData.Model;

            Assert.IsType<Client>(returnClient);

        }

        [Theory]
        [InlineData(2)]
        public async void EditAccountShow(int id)
        {
            var result = await accountController.ProfileEdit(id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditAccount()
        {

            var editAccount = new EditClientDto()
            {
                Email = "Test2@wp.pl",
                FirstName = "Tes2",
                LastName = "Test2",
                DateOfBirth = new DateTime(),
                Nationality = "Test2",
            };

            var result = await accountController.ProfileEdit(editAccount);

            Assert.IsType<ViewResult>(result);

        }



        [Fact]
        public async void SelectPictureShow()
        {
            var result = accountController.SelectPicture();
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public async void RestartPasswordShow()
        {
            var result = accountController.RestartPassword();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void RestartPassword()
        {
            var restartPassword = new RestartPasswordDto()
            {
                OldPassword = "Qwerty12#",
                NewPassword = "NewQwerty12#",
                ConfirmNewPassword = "NewQwerty12#",
            };
            
            var result = accountController.RestartPassword();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void RegisterWorker()
        {

            var addWorker = new RegisterWorkerDto()
            {
                Email = "Test2@wp.pl",
                FirstName = "Tes2",
                LastName = "Test2",
                DateOfBirth = new DateTime(),
                Nationality = "Test2",
            };

            accountController.ControllerContext.HttpContext.Request.ContentType = "application/x-www-form-urlencoded";

            var result = await accountController.RegisterWorker(addWorker);

            Assert.IsType<ViewResult>(result);

        }



        [Fact]
        public async void RegisterClient()
        {

            var addWorker = new RegisterClientDto()
            {
                Email = "Test2@wp.pl",
                FirstName = "Tes2",
                LastName = "Test2",
                DateOfBirth = new DateTime(),
                Nationality = "Test2",
            };

            accountController.ControllerContext.HttpContext.Request.ContentType = "application/x-www-form-urlencoded";


            var result = await accountController.RegisterClient(addWorker);

            Assert.IsType<ViewResult>(result);

        }
    }
}