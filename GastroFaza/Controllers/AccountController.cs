using GastroFaza.Authorization;
using GastroFaza.Exceptions;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;


namespace GastroFaza.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService service;
        private readonly RestaurantDbContext dbContext;
        private readonly IPasswordHasher<Client> passwordHasherClient;
        private readonly IPasswordHasher<Worker> passwordHasherWorker;
        private readonly AuthenticationSettings authenticationSetting;
        public AccountController(IAccountService service, RestaurantDbContext dbContext, IPasswordHasher<Client> passwordHasherClient, IPasswordHasher<Worker> passwordHasherWorker, AuthenticationSettings authenticationSetting)
        {
            this.service = service;
            this.dbContext = dbContext;
            this.passwordHasherClient = passwordHasherClient;
            this.passwordHasherWorker = passwordHasherWorker;
            this.authenticationSetting = authenticationSetting;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction("Login");
        }

        [Route("Welcome")]
        public IActionResult Welcome()
        {
            ViewBag.username = HttpContext.Session.GetString("email");
            return View("Welcome");
        }

        [HttpPost]
        [Route("registerWorker")]
        public IActionResult RegisterWorker(RegisterWorkerDto dto)
        {
            if (ModelState.IsValid)
            {
                var newWorker = new Worker()
                {
                    Email = dto.Email,
                    DateOfBirth = dto.DateOfBirth,
                    Nationality = dto.Nationality,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PasswordHash = dto.Password,
                    RoleId = dto.RoleId

                };

                var hashedPass = this.passwordHasherWorker.HashPassword(newWorker, dto.Password);

                newWorker.PasswordHash = hashedPass;
                this.dbContext.Workers.Add(newWorker);
                this.dbContext.SaveChanges();

                HttpContext.Session.SetString("email", dto.Email);
                return RedirectToAction("WelcomeWorker");
            }
            ViewBag.msg = "Invalid";
            return View("Login");
        }

        [HttpPost]
        [Route("registerClient")]
        public IActionResult RegisterClient(RegisterClientDto dto)
        {
            if (ModelState.IsValid)
            {
                var newClient = new Client()
                {
                    Email = dto.Email,
                    DateOfBirth = dto.DateOfBirth,
                    Nationality = dto.Nationality,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PasswordHash = dto.Password,

                };

                var hashedPass = this.passwordHasherClient.HashPassword(newClient, dto.Password);

                newClient.PasswordHash = hashedPass;
                this.dbContext.Clients.Add(newClient);
                this.dbContext.SaveChanges();

                return RedirectToAction("Welcome");
            }
            ViewBag.msg = "Invalid";
            return View("Register");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var worker = this.dbContext
                                .Workers
                                .Include(u => u.Role)
                                .FirstOrDefault(u => u.Email == dto.Email);
                
                var client = this.dbContext
                 .Clients
                 .FirstOrDefault(u => u.Email == dto.Email);

                //If there is no such worker or client in database
                if (worker is null && client is null)
                {
                        ViewBag.msg = "Email or password is invalid.";
                        return View("Login");
                }

                //if user is client then check for clients
                if(worker is null)
                {
                    var result1 = this.passwordHasherClient.VerifyHashedPassword(client, client.PasswordHash, dto.Password);
                    if (result1 == PasswordVerificationResult.Failed)
                    {
                        ViewBag.msg = "Email or password is invalid.";
                        return View("Login");
                    }
                    HttpContext.Session.SetString("email", dto.Email);
                    HttpContext.Session.SetString("isWorker", "isClient");
                    return RedirectToAction("Welcome");
                }
                //else check workers
                var result2 = this.passwordHasherWorker.VerifyHashedPassword(worker, worker.PasswordHash, dto.Password);
                if (result2 == PasswordVerificationResult.Failed)
                {
                    ViewBag.msg = "Email or password is invalid.";
                    return View("Login");
                }
                HttpContext.Session.SetString("email", dto.Email);
                HttpContext.Session.SetString("isWorker", "isWorker");
                return RedirectToAction("Welcome");
            }
            return View("Login");
        }


    }
    
}
