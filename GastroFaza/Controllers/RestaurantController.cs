using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GastroFaza.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    //[Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService services;
        public RestaurantController(IRestaurantService services)
        {
            this.services = services;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurants = this.services.GetAll();

            return Ok(restaurants);
        }



        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetOne([FromRoute] int id)
        {
            var restaurant = this.services.GetById(id);

            return Ok(restaurant);
        }




        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.services.Delete(id);

            return NotFound();
        }



        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto model, [FromRoute] int id)
        {
            this.services.Update(id, model);

            return Ok();
        }



        [HttpPost]
        public ActionResult Create([FromBody] CreateRestaurantDto dto)
        {

            //HttpContext.Workers.IsInRole("Admin");
            //var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = this.services.Create(dto);

            return Created($"/api/bid/{id}", null);
        }

    }
}
