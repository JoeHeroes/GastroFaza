using GastroFaza.Exceptions;
using GastroFaza.Models.DTO;
using GastroFaza.Models;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateClientDto dto);
        int Create(CreateClientDto dto);
    }
    public class ClientService : IClientService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly ILogger<ClientService> logger;

        public ClientService(RestaurantDbContext dbContext, ILogger<ClientService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public IEnumerable<Client> GetAll()
        {
            var clients = this.dbContext.Clients.ToList();

            if (clients is null)
            {
                throw new NotFoundException("Clients not found");
            }

            return clients;
        }

        public Client GetById(int id)
        {
            var client = this.dbContext.Clients.FirstOrDefault(u => u.Id == id);

            if (client is null)
            {
                throw new NotFoundException("Clients not found");
            }

            return client;

        }
        public void Delete(int id)
        {
            logger.LogWarning($"Client with id: {id} DELETE action invoked");

            var client = this.dbContext
               .Clients
               .FirstOrDefault(u => u.Id == id);

            if (client is null)
            {
                throw new NotFoundException("Client not found");
            }

            dbContext.Clients.Remove(client);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public void Update(int id, UpdateClientDto dto)
        {
            var client = this.dbContext
                 .Clients
                 .FirstOrDefault(u => u.Id == id);

            if (client is null)
            {
                throw new NotFoundException("Client not found");
            }

            client.OrderID = dto.OrderID;
            client.Email = dto.Email;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }


        public int Create(CreateClientDto dto)
        {
            var client = new Client()
            {
                
                Email = dto.Email,
                OrderID = dto.OrderID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                PasswordHash = dto.PasswordHash,
    };
            dbContext.Clients.Add(client);
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return client.Id;
        }


    }
}
