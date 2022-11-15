using GastroFaza.Models.DTO;
using GastroFaza.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastroFaza.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("registerWorker")]
        public ActionResult RegisterWorker([FromBody] RegisterWorkerDto dto) 
        {
            _service.RegisterWorker(dto);
            return Ok();
        }
        
        [HttpPost("loginWorker")]
        public ActionResult LoginWorker([FromBody] LoginDto dto)
        {
            string token = _service.GeneratJwtForWorker(dto);

            return Ok(token);
        }

        [HttpPost("registerClient")]
        public ActionResult RegisterClient([FromBody] RegisterClientDto dto)
        {
            _service.RegisterClient(dto);
            return Ok();
        }

        [HttpPost("loginClient")]
        public ActionResult LoginClient([FromBody] LoginDto dto)
        {
            string token = _service.GeneratJwtForClient(dto);

            return Ok(token);
        }
    }
}
