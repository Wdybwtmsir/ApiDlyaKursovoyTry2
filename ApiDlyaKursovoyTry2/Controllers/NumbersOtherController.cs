using ApiDlyaKursovoyTry2.Models;
using ApiDlyaKursovoyTry2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ApiDlyaKursovoyTry2.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]

    public class NumbersOtherController : ControllerBase
    {
        private readonly NumbersOtherService nmboService;

        public NumbersOtherController(NumbersOtherService service)
        {
            this.nmboService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NumbersOther>>> GetAllNumbersOther()
        {
            return Ok(await nmboService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NumbersOther>> GetNumbersOtherById(int id)
        {
            var nmbo = await nmboService.GetById(id);
            if (nmbo == null) return NotFound();
            return Ok(nmbo);
        }
        [HttpPost]
        public async Task<ActionResult<NumbersOther>> CreateNumbersOther([FromBody] NumbersOther nmbo)
        {
            await nmboService.Create(nmbo);
            return CreatedAtAction(nameof(GetNumbersOtherById), new { Id = nmbo.IdNumbersOther }, nmbo);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<NumbersOther>> UpdateNumbersOther(int id, [FromBody] NumbersOther nmbo)
        {
            if (nmbo.IdClient != id) return BadRequest();
            await nmboService.Update(nmbo);
            return Ok(nmbo);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await nmboService.Delete(id);
            return NoContent();
        }
    }
}
