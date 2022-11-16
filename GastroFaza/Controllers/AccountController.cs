using GastroFaza.Exceptions;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GastroFaza.Controllers
{
    [Route("Account")]
    public class AccountController: Controller
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

                return RedirectToAction("Welcome");
            }
            return View(dto);
        }
        
        [HttpPost]
        [Route("loginWorker")]
        public IActionResult LoginWorker(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var worker = this.dbContext
                                .Workers
                                .Include(u => u.Role)
                                .FirstOrDefault(u => u.Email == dto.Email);

                if (worker is null)
                {
                    throw new BadRequestException("Invalid username or password");
                }

                var result = this.passwordHasherWorker.VerifyHashedPassword(worker, worker.PasswordHash, dto.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    throw new BadRequestException("Invalid username or password");
                }
                return RedirectToAction("Welcome");
            }
            return View(dto);
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
            return View("Login");
        }

        [HttpPost]
        [Route("loginClient")]
        public IActionResult LoginClient(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var client = this.dbContext
                 .Clients
                 .FirstOrDefault(u => u.Email == dto.Email);

                if (client is null)
                {
                    throw new BadRequestException("Invalid username or password");
                }

                var result = this.passwordHasherClient.VerifyHashedPassword(client, client.PasswordHash, dto.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    throw new BadRequestException("Invalid username or password");
                }
                HttpContext.Session.SetString("email", dto.Email);
                return RedirectToAction("Welcome");
            }
            ViewBag.msg = "Invalid";
            return View("Login");            
        }
    }
}
