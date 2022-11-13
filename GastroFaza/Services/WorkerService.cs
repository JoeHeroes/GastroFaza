using GastroFaza.Exceptions;
using GastroFaza.Models.DTO;
using GastroFaza.Models;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Services
{
    public interface IWorkerService
    {
        IEnumerable<Worker> GetAll();
        Worker GetById(int id);
        int Create(CreateWorkerDto dto);
        void Update(int id, UpdateWorkerDto dto);
        void Delete(int id);
    }

    public class WorkerService : IWorkerService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly ILogger<WorkerService> logger;

        public WorkerService(RestaurantDbContext dbContext, ILogger<WorkerService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public IEnumerable<Worker> GetAll()
        {
            var workers = this.dbContext.Workers.ToList();

            if (workers is null)
            {
                throw new NotFoundException("Workers not found");
            }

            return workers;
        }

        public Worker GetById(int id)
        {
            var worker = this.dbContext.Workers.FirstOrDefault(u => u.Id == id);

            if (worker is null)
            {
                throw new NotFoundException("Worker not found");
            }

            return worker;

        }

        public int Create(CreateWorkerDto dto)
        {

            var worker = new Worker()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                PasswordHash = dto.PasswordHash,
                Salary = dto.Salary,
                Rating = dto.Rating,
                RoleId = dto.RoleId,
            };

            dbContext.Workers.Add(worker);
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return worker.Id;
        }

        public void Update(int id, UpdateWorkerDto dto)
        {
            var worker = this.dbContext
                 .Workers
                 .FirstOrDefault(u => u.Id == id);

            if (worker is null)
            {
                throw new NotFoundException("Worker not found");
            }

            worker.Email = dto.Email;
            worker.Salary = dto.Salary;
            worker.Rating = dto.Rating;
            worker.RoleId = dto.RoleId;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public void Delete(int id)
        {
            logger.LogWarning($"Worker with id: {id} DELETE action invoked");

            var worker = this.dbContext
               .Workers
               .FirstOrDefault(u => u.Id == id);

            if (worker is null)
            {
                throw new NotFoundException("Worker not found");
            }

            dbContext.Workers.Remove(worker);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }
    }
}