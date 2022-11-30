using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

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
        public IActionResult GetAll()
        {
            IEnumerable<Dish> dishs = this.dbContext.Dishs;

            return View(dishs);
        }

        public IActionResult AddToOrder(int dishId)
        {
            int id = int.Parse(HttpContext.Session.GetString("current order"));

            var currentOrder = this.dbContext.Orders.FirstOrDefault(u => u.Id == id);

            var dish = this.dbContext.Dishs.FirstOrDefault(u => u.Id == dishId);

            currentOrder.Dishes.Add(dish);

            currentOrder.Price += dish.Price;
            this.dbContext.SaveChanges();

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return View("GetAll");
        }

        public IActionResult GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var dish = this.dbContext.Dishs.Find(id);

            return View(dish);
        }
        public IActionResult Delete(int? id)
        {
            var dish = this.dbContext.Dishs.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            this.dbContext.Dishs.Remove(dish);
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id, DishDto modelDTO)
        {
            string stringFileName = UploadFile(modelDTO);
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Dishs.Find(id);
                model.Name = modelDTO.Name;
                model.Description = modelDTO.Description;
                model.Price = modelDTO.Price;
                model.DishType = modelDTO.DishType;
                model.ProfileImg = stringFileName;

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Index");
            }

            return View(modelDTO);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Create(DishDto modelDTO)
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
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
          

            return View("Add");
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
            var baseQuery = dbContext.Dishs.Where(x => x.Price >= option.MinPrice && x.Price <= option.MaxPrice); ;

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
    }
}
