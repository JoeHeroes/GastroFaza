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
        public async Task<IActionResult> GetAll()
        {
            var clients = await this.dbContext.Clients.ToListAsync();

            return View(clients);
        }

        public async Task<IActionResult> GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var client = await this.dbContext.Clients.FindAsync(id);


            return View(client);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var client = await this.dbContext.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            this.dbContext.Clients.Remove(client);
            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id, UpdateClientDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = await this.dbContext.Clients.FindAsync(id);
                model.OrderID = modelDTO.OrderID;
                model.Email = modelDTO.Email;

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Index");
            }

            return View(modelDTO);
        }

        public async Task<IActionResult> Create(CreateClientDto modelDTO)
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
                    PhoneNumber = modelDTO.PhoneNumber,
                    PasswordHash = modelDTO.PasswordHash,
                });

                try
                {
                    await this.dbContext.SaveChangesAsync();
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
