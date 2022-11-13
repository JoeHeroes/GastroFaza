using GastroFaza.Exceptions;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace GastroFaza.Services
{
    public interface IAddressService
    {
        IEnumerable<Address> GetAll();
        Address GetById(int id);
        void Delete(int id);
        void Update(int id, AddressDto dto);
        int Create(AddressDto dto);
    }
    public class AddressService : IAddressService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly ILogger<AddressService> logger;

        public AddressService(RestaurantDbContext dbContext, ILogger<AddressService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public IEnumerable<Address> GetAll()
        {
            var addresses = this.dbContext.Addresses.ToList();

            if (addresses is null)
            {
                throw new NotFoundException("Addresses not found");
            }

            return addresses;
        }

        public Address GetById(int id)
        {
            var address = this.dbContext.Addresses.FirstOrDefault(u => u.Id == id);

            if (address is null)
            {
                throw new NotFoundException("Address not found");
            }

            return address;

        }
        public void Delete(int id)
        {
            logger.LogWarning($"Address with id: {id} DELETE action invoked");

            var address = this.dbContext
               .Addresses
               .FirstOrDefault(u => u.Id == id);

            if (address is null)
            {
                throw new NotFoundException("Address not found");
            }

            dbContext.Addresses.Remove(address);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public void Update(int id, AddressDto dto)
        {
            var address = this.dbContext
                 .Addresses
                 .FirstOrDefault(u => u.Id == id);

            if (address is null)
            {
                throw new NotFoundException("Address not found");
            }

            address.City = dto.City;
            address.Street = dto.Street;
            address.PostalCode = dto.PostalCode;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }


        public int Create(AddressDto dto)
        {
            var address = new Address()
            {
                City = dto.City,
                Street = dto.Street,
                PostalCode = dto.PostalCode
        };
            dbContext.Addresses.Add(address);
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return address.Id;
        }


    }
}