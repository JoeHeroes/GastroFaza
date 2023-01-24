using GastroFaza.Models.DTO;
using GastroFaza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Controllers
{
    public class TableController : Controller
    {
        private readonly RestaurantDbContext dbContext;
        public TableController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [Route("SearchTable")]
        public IActionResult SearchTable(string id)
        {
            if (id == null)
            {
                IEnumerable<DiningTable> tables = this.dbContext.Tables;

                return View(tables);
            }

            var baseQuery = dbContext.Tables.Where(x => x.Id == int.Parse(id));

            return View(baseQuery);
        }

        public IActionResult Edit(int Id)
        {
            var table = this.dbContext.Tables.Where(s => s.Id == Id).FirstOrDefault();

            return View(table);
        }
        [HttpPost]
        public IActionResult Edit(int? id, TableDto modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Tables.Find(id);
                model.Seats = modelDTO.Seats;

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("SearchTable");
            }

            return View(modelDTO);
        }
        public IActionResult Delete(int? id)
        {
            var table = this.dbContext.Tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }

            this.dbContext.Tables.Remove(table);
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
            return RedirectToAction("GetAllTables");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TableDto modelDTO)
        {

            this.dbContext.Tables.Add(new DiningTable()
            {
                Busy=false,
                Seats=modelDTO.Seats,
            });

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }


            return RedirectToAction("GetAllTables");
        }


    }
}
