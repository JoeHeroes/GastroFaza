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
            if (HttpContext.Session.GetString("email") != null)
            {
                IEnumerable<Reservation> reservations = this.dbContext.Reservations;

                if (HttpContext.Session.GetString("isWorker") != "true")
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
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null)
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
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                return View();
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Edit(int? id, ReservationDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Reservations.Find(id);
                model.TableId = modelDTO.TableId;
                model.DataOfReservation = new DateTime(modelDTO.DateOfReservation.Year,
                    modelDTO.DateOfReservation.Month, 
                    modelDTO.DateOfReservation.Day, 
                    modelDTO.HourOfReservation.Hour, 
                    modelDTO.HourOfReservation.Minute, 
                    modelDTO.HourOfReservation.Second);
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
            if (HttpContext.Session.GetString("email") != null)
            {
                return View();
            }
            else {
                return RedirectToAction("Login", "Account");
            }
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
                    DataOfReservation = new DateTime(modelDTO.DateOfReservation.Year,
                    modelDTO.DateOfReservation.Month, 
                    modelDTO.DateOfReservation.Day, 
                    modelDTO.HourOfReservation.Hour, 
                    modelDTO.HourOfReservation.Minute, 
                    modelDTO.HourOfReservation.Second)
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
