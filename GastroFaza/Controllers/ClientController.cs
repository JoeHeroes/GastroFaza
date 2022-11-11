using GastroFaza.Models.DTO;
using GastroFaza.Models;
using GastroFaza.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastroFaza.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService services;
        public ClientController(IClientService services)
        {
            this.services = services;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetAll()
        {
            var clients = this.services.GetAll();

            return Ok(clients);
        }



        [HttpGet("{id}")]
        public ActionResult<Client> GetOne([FromRoute] int id)
        {
            var client = this.services.GetById(id);

            return Ok(client);
        }




        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.services.Delete(id);

            return NotFound();
        }



        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateClientDto model, [FromRoute] int id)
        {
            this.services.Update(id, model);

            return Ok();
        }



        [HttpPost]
        public ActionResult Create([FromBody] CreateClientDto dto)
        {
            var id = this.services.Create(dto);

            return Created($"/api/bid/{id}", null);
        }
    }
}
