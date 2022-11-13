using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GastroFaza.Controllers
{
    [Route("api/address")]
    [ApiController]
    //[Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService services;
        public AddressController(IAddressService services)
        {
            this.services = services;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Address>> GetAll()
        {
            var addresses = this.services.GetAll();

            return Ok(addresses);
        }



        [HttpGet("{id}")]
        public ActionResult<Address> GetOne([FromRoute] int id)
        {
            var address = this.services.GetById(id);

            return Ok(address);
        }




        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.services.Delete(id);

            return NotFound();
        }



        [HttpPut("{id}")]
        public ActionResult Update([FromBody] AddressDto model, [FromRoute] int id)
        {
            this.services.Update(id, model);

            return Ok();
        }



        [HttpPost]
        public ActionResult Create([FromBody] AddressDto dto)
        {

            //HttpContext.Workers.IsInRole("Admin");
            //var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = this.services.Create(dto);

            return Created($"/api/bid/{id}", null);
        }

    }
}
