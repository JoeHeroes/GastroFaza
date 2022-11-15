using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class ClientController : Controller
    {
        private readonly RestaurantDbContext dbContext;

        public ClientController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult GetAll()
        {
            IEnumerable<Client> clients = this.dbContext.Clients;

            return View(clients);
        }

        public IActionResult GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var client = this.dbContext.Clients.Find(id);

            return View(client);
        }
        public IActionResult Delete(int? id)
        {
            var client = this.dbContext.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            this.dbContext.Clients.Remove(client);
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

        public IActionResult Edit(int? id, UpdateClientDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Clients.Find(id);
                model.OrderID = modelDTO.OrderID;
                model.Email = modelDTO.Email;

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

        public IActionResult Create(CreateClientDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                this.dbContext.Clients.Add(new Client()
                {
                    Email = modelDTO.Email,
                    OrderID = modelDTO.OrderID,
                    FirstName = modelDTO.FirstName,
                    LastName = modelDTO.LastName,
                    DateOfBirth = modelDTO.DateOfBirth,
                    Nationality = modelDTO.Nationality,
                    PasswordHash = modelDTO.PasswordHash,
                });

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
    }
}
