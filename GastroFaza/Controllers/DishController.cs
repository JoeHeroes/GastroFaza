using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class DishController : Controller
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IWebHostEnvironment webHost;
        public DishController(RestaurantDbContext dbContext, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }
        public async Task<IActionResult> GetAll()
        {
            var dishs = await this.dbContext.Dishs.ToListAsync();

            return View(dishs);
        }
 

        public async Task<IActionResult> AddToOrder(int dishId)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("IsWorker") == "false" || HttpContext.Session.GetString("Role") != "Cook")
                {

                    int id = int.Parse(HttpContext.Session.GetString("current order"));

                    var currentOrder = await this.dbContext.Orders.FirstOrDefaultAsync(u => u.Id == id);

                    var dish = await this.dbContext.Dishs.FirstOrDefaultAsync(u => u.Id == dishId);

                    currentOrder.Price += dish.Price;

                    var dishOrder = new DishOrder()
                    {
                        DishMany = dish,
                        OrderMany = currentOrder,
                    };

                    this.dbContext.DishOrders.Add(dishOrder);

                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return View("GetAll");
                }
                return Forbid();

            }
            return RedirectToAction("Login", "Account");
    }

        public async Task<IActionResult> GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var dish = await this.dbContext.Dishs.FindAsync(id);

            return View(dish);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var dish = await this.dbContext.Dishs.FindAsync(id);
                    if (dish == null)
                    {
                        return NotFound();
                    }

                    this.dbContext.Dishs.Remove(dish);
                    try
                    {
                        await this.dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new DbUpdateException("Error DataBase", e);
                    }
                    return RedirectToAction("GetAllDishes");
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }
        
        public async Task<IActionResult> Edit(int Id)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    var dish = await this.dbContext.Dishs.FirstOrDefaultAsync(s => s.Id == Id);

                    var dishDto = new DishDto()
                    {
                        Id = dish.Id,
                        Name = dish.Name,
                        Description = dish.Description,
                        Price = dish.Price,
                        DishType = dish.DishType,
                    };

                    return View(dishDto);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int? id, DishDto modelDTO)
        {
            string stringFileName = UploadFile(modelDTO);
            if (ModelState.IsValid)
            {
                var model = await this.dbContext.Dishs.FindAsync(id);
                model.Name = modelDTO.Name;
                model.Description = modelDTO.Description;
                model.Price = modelDTO.Price;
                model.DishType = modelDTO.DishType;
                model.ProfileImg = stringFileName;

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("SearchMenu");
            }

            return View(modelDTO);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    return View();
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> Create(DishDto modelDTO)
        {

            string stringFileName = UploadFile(modelDTO);
           
                this.dbContext.Dishs.Add(new Dish()
                {
                    Name = modelDTO.Name,
                    Description = modelDTO.Description,
                    Price = modelDTO.Price,
                    DishType = modelDTO.DishType,
                    ProfileImg = stringFileName,
                });

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }


            return RedirectToAction("GetAllDishes");
        }


        private string UploadFile(DishDto dto)
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

        [Route("Search")]
        public IActionResult Search(OptionFilterDto option)
        {
            var baseQuery = this.dbContext.Dishs.Where(x => x.Price >= option.MinPrice && x.Price <= option.MaxPrice); ;

            if (option.SearchString != "")
            {
                var baseProducer = baseQuery.Where(x => x.Name == option.SearchString);
                baseQuery = baseProducer;
            }

            if (option.Dish != DishType.none)
            {
                var baseProducer = baseQuery.Where(x => x.DishType == option.Dish);
                baseQuery = baseProducer;
            }
            return View(baseQuery);
        }



        [Route("SearchMenu")]
        public async Task<IActionResult> SearchMenu(string name)
        {

            if (String.IsNullOrEmpty(name))
            {
                var dishs = await this.dbContext.Dishs.ToListAsync();

                return View(dishs);
            }

            var baseQuery = this.dbContext.Dishs.Where(x => x.Name == name);

            if (baseQuery is null)
            {
                var baseProducer = baseQuery.Where(x => x.DishType.ToString() == name);
                baseQuery = baseProducer;
            }

            return View(baseQuery);
        }


    }
}
