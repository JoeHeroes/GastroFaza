using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GastroFaza.Controllers
{
    [Route("api/dish")]
    [ApiController]
    //[Authorize]
    public class DishController : ControllerBase
    {
        private readonly IDishService services;
        public DishController(IDishService services)
        {
            this.services = services;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Dish>> GetAll()
        {
            var dishs = this.services.GetAll();

            return Ok(dishs);
        }



        [HttpGet("{id}")]
        public ActionResult<Dish> GetOne([FromRoute] int id)
        {
            var dish = this.services.GetById(id);

            return Ok(dish);
        }




        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.services.Delete(id);

            return NotFound();
        }



        [HttpPut("{id}")]
        public ActionResult Update([FromBody] DishDto model, [FromRoute] int id)
        {
            this.services.Update(id, model);

            return Ok();
        }



        [HttpPost]
        public ActionResult Create([FromBody] DishDto dto)
        {

            //HttpContext.Workers.IsInRole("Admin");
            //var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = this.services.Create(dto);

            return Created($"/api/bid/{id}", null);
        }

    }
}
