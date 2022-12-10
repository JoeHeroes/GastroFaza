using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class ReservationController : Controller
    {
        private readonly RestaurantDbContext dbContext;

        public ReservationController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult GetAll()
        {
            IEnumerable<Reservation> reservations = this.dbContext.Reservations;
    
            if (HttpContext.Session.GetString("isWorker") != "true" && HttpContext.Session.GetString("email") != null)
            {
                string userEmail = HttpContext.Session.GetString("email");
                var client = this.dbContext.Clients.FirstOrDefault(u => u.Email == userEmail);
                IEnumerable<Reservation> clientReservations = reservations.Where(r => r.ClientId == client.Id);
                return View(clientReservations);
            }
            else
            {
                return View(reservations);
            }


        }

        public IActionResult GetOne(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var reservation = this.dbContext.Reservations.Find(id);

            return View(reservation);
        }
        public IActionResult Delete(int? id)
        {
            var reservation = this.dbContext.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            this.dbContext.Reservations.Remove(reservation);
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return RedirectToAction("GetAll");
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int? id, ReservationDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Reservations.Find(id);
                model.TableId = modelDTO.TableId;
                model.DataOfReservation = modelDTO.DataOfReservation;

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("GetAll");
            }

            return View(modelDTO);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReservationDto modelDTO)
        {
            string userEmail = HttpContext.Session.GetString("email");
            var client = this.dbContext.Clients.FirstOrDefault(u => u.Email == userEmail);
            int idClient = client.Id;

            if (ModelState.IsValid)
            {
                this.dbContext.Reservations.Add(new Reservation()
                {
                    ClientId = idClient,
                    TableId = modelDTO.TableId,
                    DataOfReservation = modelDTO.DataOfReservation
                });

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                return RedirectToAction("GetAll");
            }

            return View(modelDTO);
        }
    }
}
