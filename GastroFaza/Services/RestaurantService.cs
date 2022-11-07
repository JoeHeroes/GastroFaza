using GastroFaza.Exceptions;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace GastroFaza.Services
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto model);
        int Create(CreateRestaurantDto dto);
    }
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly ILogger<RestaurantService> logger;

        public RestaurantService(RestaurantDbContext dbContext, ILogger<RestaurantService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            var restaurants = this.dbContext.Restaurants.ToList();

            if (restaurants is null)
            {
                throw new NotFoundException("Restaurants not found");
            }

            return restaurants;
        }

        public Restaurant GetById(int id)
        {
            var restaurant = this.dbContext.Restaurants.FirstOrDefault(u => u.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            return restaurant;

        }
        public void Delete(int id)
        {
            logger.LogWarning($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = this.dbContext
               .Restaurants
               .FirstOrDefault(u => u.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            dbContext.Restaurants.Remove(restaurant);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public void Update(int id, UpdateRestaurantDto model)
        {
            var restaurant = this.dbContext
                 .Restaurants
                 .FirstOrDefault(u => u.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            
            restaurant.Name = model.Name;
            restaurant.HasDelivery = model.HasDelivery;
            restaurant.Description = model.Description;
            

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }


        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = new Restaurant()
            {
                Name = dto.Name,
                Description = dto.Description,
                HasDelivery = dto.HasDelivery,
                ContactEmail = dto.ContactEmail,
                ContactNumber = dto.ContactNumber,
                Address = new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    PostalCode = dto.PostalCode
                }
            };
            dbContext.Restaurants.Add(restaurant);
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return restaurant.Id;
        }


    }
}
