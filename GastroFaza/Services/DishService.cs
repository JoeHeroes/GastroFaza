
using GastroFaza.Exceptions;
using GastroFaza.Models;
using GastroFaza.Models.DTO; 
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Services
{
    public interface IDishService
    {
        IEnumerable<Dish> GetAll();
        Dish GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateDishDto dto);
        int Create(UpdateDishDto dto);
    }
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly ILogger<RestaurantService> logger;

        public DishService(RestaurantDbContext dbContext, ILogger<RestaurantService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public IEnumerable<Dish> GetAll()
        {
            var dishs = this.dbContext.Dishs.ToList();

            if (dishs is null)
            {
                throw new NotFoundException("Dishs not found");
            }

            return dishs;
        }

        public Dish GetById(int id)
        {
            var dish = this.dbContext.Dishs.FirstOrDefault(u => u.Id == id);

            if (dish is null)
            {
                throw new NotFoundException("Dish not found");
            }

            return dish;

        }
        public void Delete(int id)
        {
            logger.LogWarning($"Dish with id: {id} DELETE action invoked");

            var dish = this.dbContext
               .Dishs
               .FirstOrDefault(u => u.Id == id);

            if (dish is null)
            {
                throw new NotFoundException("Dish not found");
            }

            dbContext.Dishs.Remove(dish);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public void Update(int id, UpdateDishDto dto)
        {
            var dish = this.dbContext
                 .Dishs
                 .FirstOrDefault(u => u.Id == id);

            if (dish is null)
            {
                throw new NotFoundException("Dish not found");
            }

            dish.Name = dto.Name;
            dish.Description = dto.Description;
            dish.Price = dto.Price;
            dish.DishType = dto.DishType;


            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }


        public int Create(UpdateDishDto dto)
        {
            var dish = new Dish()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DishType = dto.DishType
            };
            dbContext.Dishs.Add(dish);
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return dish.Id;
        }
    }
}