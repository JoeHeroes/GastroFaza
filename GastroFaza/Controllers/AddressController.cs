using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GastroFaza.Controllers
{
    public class AddressController : Controller
    {
        private readonly RestaurantDbContext dbContext;
        public AddressController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Address> addresses = this.dbContext.Addresses;

            return View(addresses);
        }



        public IActionResult GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var address = this.dbContext.Addresses.Find(id);

            return View(address);
        }




        
        public IActionResult Delete(int? id)
        {
            var address = this.dbContext.Addresses.Find(id);
            if (address == null)
            {
                
                return NotFound();
            }

            this.dbContext.Addresses.Remove(address);
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



        public IActionResult Edit(int? id,AddressDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Addresses.Find(id);
                model.City = modelDTO.City;
                model.Street = modelDTO.Street;
                model.PostalCode = modelDTO.PostalCode;

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



        public IActionResult Create([FromBody] AddressDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                this.dbContext.Addresses.Add(new Address()
                {
                    City = modelDTO.City,
                    Street = modelDTO.Street,
                    PostalCode = modelDTO.PostalCode,
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
