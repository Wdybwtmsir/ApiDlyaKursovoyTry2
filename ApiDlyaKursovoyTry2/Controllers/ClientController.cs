using ApiDlyaKursovoyTry2.Models;
using ApiDlyaKursovoyTry2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace APIdlyaKursovoy.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]

    public class ClientController : ControllerBase
    {
        private readonly ClientService clieService;

        public ClientController(ClientService service)
        {
            this.clieService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            return Ok(await clieService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var clie = await clieService.GetById(id);
            if (clie == null) return NotFound();
            return Ok(clie);
        }
        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient([FromBody] Client clie)
        {
            await clieService.Create(clie);
            return CreatedAtAction(nameof(GetClientById), new { Id = clie.IdClient }, clie);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> UpdateClient(int id, [FromBody] Client clie)
        {
            if (clie.IdClient != id) return BadRequest();
            await clieService.Update(clie);
            return Ok(clie);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await clieService.Delete(id);
            return NoContent();
        }
    }
}
