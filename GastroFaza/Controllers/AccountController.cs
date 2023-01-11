using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GastroFaza.Controllers
{
    [Route("Account")] 
    public class AccountController : Controller      
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IPasswordHasher<Client> passwordHasherClient;
        private readonly IPasswordHasher<Worker> passwordHasherWorker;
        private readonly AuthenticationSettings authenticationSetting;
        public AccountController(RestaurantDbContext dbContext, IPasswordHasher<Client> passwordHasherClient, IPasswordHasher<Worker> passwordHasherWorker, AuthenticationSettings authenticationSetting)
        {
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
            var nationsList = NationsList.GetNations();
            var model = new RegisterClientDto();
            model.SelectedNations = new List<SelectListItem>();
            foreach (var nation in nationsList)
            {
                model.SelectedNations.Add(new SelectListItem() { Text = nation.Name, Value = nation.Name });
            }
            return View(model);
        }
        [Route("CreateWorkerAccount")]      //for manager
        public IActionResult CreateWorkerAccount()
        {
            var nationsList = NationsList.GetNations();
            var model = new RegisterWorkerDto();
            model.SelectedNations = new List<SelectListItem>();
            model.Roles = new List<SelectListItem>();
            foreach (var nation in nationsList)
            {
                model.SelectedNations.Add(new SelectListItem() { Text = nation.Name, Value = nation.Name });
            }

            foreach(var role in dbContext.Roles.ToList())
            {
                model.Roles.Add(new SelectListItem() { Text = role.Name, Value = role.Id.ToString() });
            }
            return View(model); ;
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("email");
            return RedirectToAction("Login");
        }

        [Route("Welcome")]
        public IActionResult Welcome()
        {
            ViewBag.username = HttpContext.Session.GetString("email");
            return View("Welcome");
        }
        [Route("AccountSettings")]
        public IActionResult AccountSettings()
        {
            var account = dbContext.Clients.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
            return View(account);
        }

        [HttpPost]
        [Route("AccountSettings")]
        public IActionResult AccountSettings(EditClientDto dto)
        {
            if (ModelState.IsValid)
            {
                var account = dbContext.Clients.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
                account.Nationality = dto.Nationality;
                account.FirstName = dto.FirstName;
                account.LastName = dto.LastName;
                account.DateOfBirth = dto.DateOfBirth;

                dbContext.SaveChanges();
            }
            return View("AccountSettings");
        }


        



        [Route("RestartPassword")]
        public IActionResult RestartPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("RestartPassword")]
        public IActionResult RestartPassword(RestartPasswordDto dto)
        {

            if (ModelState.IsValid)
            {

                if (dto.NewPassword != dto.ConfirmNewPassword)
                {
                    ViewBag.msg = "New Password must be the same.";
                    return View("RestartPassword");
                }


                if (dto.OldPassword == dto.NewPassword)
                {
                    ViewBag.msg = "New and Old Password and couldn't be the same";
                    return View("RestartPassword");
                }


                if (HttpContext.Session.GetString("isWorker") != "true")
                {
                    var client = this.dbContext
                                 .Clients
                                 .FirstOrDefault(u => u.Email == HttpContext.Session.GetString("email"));

                    var result1 = this.passwordHasherClient.VerifyHashedPassword(client, client.PasswordHash, dto.OldPassword);
                    if (result1 == PasswordVerificationResult.Failed)
                    {
                        ViewBag.msg = "Old password is invalid.";
                        return View("RestartPassword");
                    }

                    client.PasswordHash = this.passwordHasherClient.HashPassword(client, dto.NewPassword); ;
                    this.dbContext.SaveChanges();

                    return RedirectToAction("Welcome");
                }
                else
                {
                    var worker = this.dbContext
                                .Workers
                                .Include(u => u.Role)
                                .FirstOrDefault(u => u.Email == HttpContext.Session.GetString("email"));

                    var result2 = this.passwordHasherWorker.VerifyHashedPassword(worker, worker.PasswordHash, dto.OldPassword);
                    if (result2 == PasswordVerificationResult.Failed)
                    {
                        ViewBag.msg = "Old password is invalid.";
                        return View("RestartPassword");
                    }

                    worker.PasswordHash = this.passwordHasherWorker.HashPassword(worker, dto.NewPassword); ;
                    this.dbContext.SaveChanges();

                    return RedirectToAction("Welcome");
                }
            }
            return View("RestartPassword");
        }



        [HttpPost]
        [Route("registerWorker")]         //for manager
        public IActionResult RegisterWorker(RegisterWorkerDto dto)
        {
            //captcha validation
            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6LdjRX4jAAAAAN0GPdgW5aHuwvu-8T-V_LFzeOr8";
            bool IsCaptchaValid = (ReCaptchaClass.Validate(response) == "true" ? true : false);

            if (!IsCaptchaValid)
            {
                return View("CreateWorkerAccount");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                ViewBag.msg = "Invalid Password";
                return View("CreateWorkerAccount");
            }

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
                    RoleId = int.Parse(dto.RoleId)
                };

                var hashedPass = this.passwordHasherWorker.HashPassword(newWorker, dto.Password);

                newWorker.PasswordHash = hashedPass;
                this.dbContext.Workers.Add(newWorker);
                this.dbContext.SaveChanges();
                if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("Role") == "Manager")
                    return RedirectToAction("GetAll","Worker");
                else return View("CreateWorkerAccount");
            }
            ViewBag.msg = "Invalid";
            return View("CreateWorkerAccount");
        }

        [HttpPost]
        [Route("registerClient")]
        public IActionResult RegisterClient(RegisterClientDto dto)
        {
            //captcha validation
            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6LdjRX4jAAAAAN0GPdgW5aHuwvu-8T-V_LFzeOr8";
            bool IsCaptchaValid = (ReCaptchaClass.Validate(response) == "true" ? true : false);

            if (!IsCaptchaValid)
            {
                return View("Register");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                ViewBag.msg = "Invalid Password";
                return View("Register");
            }

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
            //captcha for login
            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6LdjRX4jAAAAAN0GPdgW5aHuwvu-8T-V_LFzeOr8";
            bool IsCaptchaValid = (ReCaptchaClass.Validate(response) == "true" ? true : false);

            if (!IsCaptchaValid)
            {
                return View("Login");
            }

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
                    HttpContext.Session.SetString("id", client.Id.ToString());
                    HttpContext.Session.SetString("email", dto.Email);
                    HttpContext.Session.SetString("isWorker", "false");
                    return RedirectToAction("Welcome");
                }
                //else check workers
                var result2 = this.passwordHasherWorker.VerifyHashedPassword(worker, worker.PasswordHash, dto.Password);
                if (result2 == PasswordVerificationResult.Failed)
                {
                    ViewBag.msg = "Email or password is invalid.";
                    return View("Login");
                }
                HttpContext.Session.SetString("id", worker.Id.ToString());
                HttpContext.Session.SetString("email", dto.Email);
                HttpContext.Session.SetString("isWorker", "true");
                if (worker.RoleId == 1)                                      //set worker role in session 
                    HttpContext.Session.SetString("Role", "Waiter");
                else if (worker.RoleId == 2)
                    HttpContext.Session.SetString("Role", "Cook");
                else
                    HttpContext.Session.SetString("Role", "Manager");
                return RedirectToAction("Welcome");
            }
            return View("Login");
        }


    }

}


//captcha validation
    public class ReCaptchaClass
{
    public static string Validate(string EncodedResponse)
    {
        var client = new System.Net.WebClient();

        string PrivateKey = "6LdjRX4jAAAAAN0GPdgW5aHuwvu-8T-V_LFzeOr8";

        var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

        var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);

        return captchaResponse.Success.ToLower();
    }

    [JsonProperty("success")]
    public string Success
    {
        get { return m_Success; }
        set { m_Success = value; }
    }

    private string m_Success;
    [JsonProperty("error-codes")]
    public List<string> ErrorCodes
    {
        get { return m_ErrorCodes; }
        set { m_ErrorCodes = value; }
    }


    private List<string> m_ErrorCodes;
}