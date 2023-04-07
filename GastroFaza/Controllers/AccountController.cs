using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly IWebHostEnvironment webHost;
        public AccountController(RestaurantDbContext dbContext, IPasswordHasher<Client> passwordHasherClient, IPasswordHasher<Worker> passwordHasherWorker, AuthenticationSettings authenticationSetting, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.passwordHasherClient = passwordHasherClient;
            this.passwordHasherWorker = passwordHasherWorker;
            this.authenticationSetting = authenticationSetting;
            this.webHost = webHost;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View();
            } else
            {
                return RedirectToAction("Welcome");
            }
        }

        [Route("Register")]
        public IActionResult Register()
        {
            var model = new RegisterClientDto();           
            return View(model);
        }

        [Route("CreateWorkerAccount")]      //for manager
        public async Task<IActionResult> CreateWorkerAccount()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var model = new RegisterWorkerDto();
                    model.Roles = new List<SelectListItem>();           

                    foreach(var role in await this.dbContext.Roles.ToListAsync())
                    {
                        model.Roles.Add(new SelectListItem() { Text = role.Name, Value = role.Id.ToString() });
                    }
                    return View(model);
                }
                return Forbid();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("isWorker");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("current order");
            HttpContext.Session.Remove("Rating");
            return RedirectToAction("Login");
        }

        [Route("Welcome")]
        public IActionResult Welcome()
        {
            ViewBag.username = HttpContext.Session.GetString("email");
            return View("Welcome");
        }

        [Route("Profile")]
        public async Task<IActionResult> Profile()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "false")
                {
                    var account = await this.dbContext.Clients.FirstOrDefaultAsync(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
                    return View(account);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }


        [Route("ProfileEdit")]
        public async Task<IActionResult> ProfileEdit(int id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "false")
                {
                    var account = await this.dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);

                    return View(account);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [Route("ProfileEdit")]
        public async Task<IActionResult> ProfileEdit(EditClientDto dto)
        {
            if (ModelState.IsValid)
            {
                var account = await this.dbContext.Clients.FirstOrDefaultAsync(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
                account.Nationality = dto.Nationality;
                account.FirstName = dto.FirstName;
                account.LastName = dto.LastName;
                account.DateOfBirth = dto.DateOfBirth;

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
            }
            return View("ProfileEdit");
        }

        [Route("SelectPicture")]
        public IActionResult SelectPicture()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "false")
                {
                    var dto = new PictureDto();

                    return View(dto);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [Route("SelectPicture")]
        public async Task<IActionResult> SelectPicture(PictureDto dto)
        {
            string stringFileName = UploadFile(dto);
            if (ModelState.IsValid)
            {
                var model =  await this.dbContext.Clients.FirstOrDefaultAsync(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
                model.ProfileImg = stringFileName;

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Profile");
            }

            return View(dto);

        }


        private string UploadFile(PictureDto dto)
        {
            string fileName = null;
            if (dto.PathPic != null)
            {
                string uploadDir = Path.Combine(webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + dto.PathPic.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dto.PathPic.CopyTo(fileStream);
                }
            }
            return fileName;
        }



        [Route("RestartPassword")]
        public IActionResult RestartPassword()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "false")
                {
                    return View();
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [Route("RestartPassword")]
        public async Task<IActionResult> RestartPassword(RestartPasswordDto dto)
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
                    var client = await this.dbContext
                                 .Clients
                                 .FirstOrDefaultAsync(u => u.Email == HttpContext.Session.GetString("email"));

                    var result1 = this.passwordHasherClient.VerifyHashedPassword(client, client.PasswordHash, dto.OldPassword);
                    if (result1 == PasswordVerificationResult.Failed)
                    {
                        ViewBag.msg = "Old password is invalid.";
                        return View("RestartPassword");
                    }

                    client.PasswordHash = this.passwordHasherClient.HashPassword(client, dto.NewPassword); ;
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }

                    return RedirectToAction("Welcome");
                }
                else
                {
                    var worker = await this.dbContext
                                .Workers
                                .Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.Email == HttpContext.Session.GetString("email"));

                    var result2 = this.passwordHasherWorker.VerifyHashedPassword(worker, worker.PasswordHash, dto.OldPassword);
                    if (result2 == PasswordVerificationResult.Failed)
                    {
                        ViewBag.msg = "Old password is invalid.";
                        return View("RestartPassword");
                    }

                    worker.PasswordHash = this.passwordHasherWorker.HashPassword(worker, dto.NewPassword); ;
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }

                    return RedirectToAction("Welcome");
                }
            }
            return View("RestartPassword");
        }



        [HttpPost]
        [Route("registerWorker")]         //for manager
        public async Task<IActionResult> RegisterWorker(RegisterWorkerDto dto)
        {
            //captcha validation
            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6LdjRX4jAAAAAN0GPdgW5aHuwvu-8T-V_LFzeOr8";
            /*bool IsCaptchaValid = (ReCaptchaClass.Validate(response) == "true" ? true : false);

            if (!IsCaptchaValid)
            {
                return View("CreateWorkerAccount");
            }*/

            var worker = await this.dbContext
                                .Workers
                                .Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (worker != null)
            {
                ViewBag.msg = "Email is taken";
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
                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("Role") == "Manager")
                    return RedirectToAction("GetAll","Worker");
                else return View("CreateWorkerAccount");
            }
            ViewBag.msg = "Invalid";
            return View("CreateWorkerAccount");
        }

        [HttpPost]
        [Route("registerClient")]
        public async Task<IActionResult> RegisterClient(RegisterClientDto dto)
        {
            //captcha validation
            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6LdjRX4jAAAAAN0GPdgW5aHuwvu-8T-V_LFzeOr8";
            /*bool IsCaptchaValid = (ReCaptchaClass.Validate(response) == "true" ? true : false);

            if (!IsCaptchaValid)
            {
                return View("Register");
            }*/

            var client = await this.dbContext
                               .Clients
                               .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (client != null)
            {
                ViewBag.msg = "Email is taken";
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
                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Welcome");
            }
            ViewBag.msg = "Invalid";
            return View("Register");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            //captcha for login
            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6LdjRX4jAAAAAN0GPdgW5aHuwvu-8T-V_LFzeOr8";
            /*bool IsCaptchaValid = (ReCaptchaClass.Validate(response) == "true" ? true : false);

            if (!IsCaptchaValid)
            {
                return View("Login");
            }*/

            if (ModelState.IsValid)
            {
                var worker = await this.dbContext
                                .Workers
                                .Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.Email == dto.Email);
                
                var client = await this.dbContext
                             .Clients
                             .FirstOrDefaultAsync(u => u.Email == dto.Email);

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
                HttpContext.Session.SetString("Rating",worker.Rating.ToString());
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

/*
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
}*/