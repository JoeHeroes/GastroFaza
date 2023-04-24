using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> GetAll()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                var reservations = await this.dbContext.Reservations.ToListAsync();

                if (HttpContext.Session.GetString("isWorker") != "true")
                {
                    string userEmail = HttpContext.Session.GetString("email");
                    var client = await this.dbContext.Clients.FirstOrDefaultAsync(u => u.Email == userEmail);
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
        public async Task<IActionResult> GetAllReservations()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
                {
                    var reservations = await this.dbContext.Reservations.ToListAsync();

                    return View(reservations);
                }
                return Forbid();
            }
            return RedirectToAction("Login", "Account");
        }

        [Route("Check")]
        public IActionResult Check()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        [Route("Choice")]
        public async Task<IActionResult> Choice(CheckReservationDto dto)
        {
            var reservation = this.dbContext.Reservations.Where(x => x.DataOfReservation.Date == dto.DateOfReservation.Date && x.DataOfReservation.TimeOfDay == dto.HourOfReservation.TimeOfDay);

            var create = new ReservationDto();
            var list = new List<SelectListItem>();
            var resList = new List<int>();

            foreach (var r in reservation)
            {
                resList.Add(r.TableId[0]);
            }

            var tabel = await this.dbContext.Tables.ToListAsync();

            var res = 0;

            for (int i = 1; i <= tabel.Count(); i++)
            {
                res = resList.Find(x => x == i);
                if (res == 0)
                {
                    list.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
                }
            }
            create.DateOfReservation = dto.DateOfReservation;
            create.HourOfReservation = dto.HourOfReservation;
            create.TableSelect = list;

            return View(create);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ReservationDto dto)
        {
            string userEmail = HttpContext.Session.GetString("email");
            var client = await this.dbContext.Clients.FirstOrDefaultAsync(u => u.Email == userEmail);
            int idClient = client.Id;

            if (ModelState.IsValid)
            {
                this.dbContext.Reservations.Add(new Reservation()
                {
                    ClientId = idClient,
                    TableId = dto.TableId,
                    DataOfReservation = new DateTime(dto.DateOfReservation.Year,
                    dto.DateOfReservation.Month,
                    dto.DateOfReservation.Day,
                    dto.HourOfReservation.Hour,
                    dto.HourOfReservation.Minute,
                    dto.HourOfReservation.Second)
                });

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                return RedirectToAction("GetAll");
            }

            return View(dto);
        }

        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
               
                if (HttpContext.Session.GetString("isWorker") != "true")
                {
                    return View();
                }
                return Forbid();
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, ReservationDto dto)
        {
            
            if (ModelState.IsValid)
            {
                var model = await this.dbContext.Reservations.FindAsync(id);
                if (model == null)
                {
                    return NotFound();
                }


                if (model.ClientId.ToString() != HttpContext.Session.GetString("id"))
                {
                    return Forbid();
                }


                model.TableId = dto.TableId;
                model.DataOfReservation = new DateTime(dto.DateOfReservation.Year,
                    dto.DateOfReservation.Month, 
                    dto.DateOfReservation.Day, 
                    dto.HourOfReservation.Hour, 
                    dto.HourOfReservation.Minute, 
                    dto.HourOfReservation.Second);
                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("GetAll");
            }

            return View(dto);
        }


        public IActionResult WorkerCreate()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if(HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
                {
                    return View();
                }
                return Forbid();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> WorkerCreate(ReservationWorkerDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                this.dbContext.Reservations.Add(new Reservation()
                {
                    ClientId = modelDTO.ClientId,
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
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                return RedirectToAction("GetAll");
            }

            return View(modelDTO);
        }

        public IActionResult WorkerEdit()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("isWorker") == "true" && HttpContext.Session.GetString("Role") != "Cook")
                {
                    return View();
                }
                return Forbid();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> WorkerEdit(int? id, ReservationWorkerDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = await this.dbContext.Reservations.FindAsync(id);

                if (model == null)
                {
                    return NotFound();
                }

                model.ClientId = modelDTO.ClientId;
                model.TableId = modelDTO.TableId;
                model.DataOfReservation = new DateTime(modelDTO.DateOfReservation.Year,
                    modelDTO.DateOfReservation.Month,
                    modelDTO.DateOfReservation.Day,
                    modelDTO.HourOfReservation.Hour,
                    modelDTO.HourOfReservation.Minute,
                    modelDTO.HourOfReservation.Second);
                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("GetAll");
            }

            return View(modelDTO);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("Role") != "true")
            {
                var reservation = await this.dbContext.Reservations.FindAsync(id);

                if (reservation == null)
                {
                    return NotFound();
                }

                if(reservation.ClientId.ToString() != HttpContext.Session.GetString("id"))
                {
                    return Forbid();
                }

                this.dbContext.Reservations.Remove(reservation);
                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                return RedirectToAction("GetAll");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
