using GastroFaza.Models.DTO;
using GastroFaza.Models;
using GastroFaza.Services;
using Microsoft.AspNetCore.Mvc;
using GastroFaza.Exceptions;

namespace GastroFaza.Controllers
{
    [Route("api/worker")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService services;
        public WorkerController(IWorkerService services)
        {
            this.services = services;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Worker>> GetAll()
        {
            var workers = this.services.GetAll();

            return Ok(workers);
        }

        [HttpGet("{id}")]
        public ActionResult<Worker> GetOne([FromRoute] int id)
        {
            var worker = this.services.GetById(id);

            return Ok(worker);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateWorkerDto dto)
        {

            if(!ModelState.IsValid)
            {
                throw new BadRequestException(ModelState.ToString());
            }

            var id = this.services.Create(dto);

            return Created($"/api/worker/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateWorkerDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(ModelState.ToString());
            }

            this.services.Update(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.services.Delete(id);

            return NotFound();
        }
    }
}