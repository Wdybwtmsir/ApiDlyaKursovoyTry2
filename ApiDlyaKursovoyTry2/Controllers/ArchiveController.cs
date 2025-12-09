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

    public class ArchiveController : ControllerBase
    {
        private readonly ArchiveService archService;

        public ArchiveController(ArchiveService service)
        {
            this.archService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archive>>> GetAllArchives()
        {
            return Ok(await archService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Archive>> GetArchiveById(int id)
        {
            var arch = await archService.GetById(id);
            if (arch == null) return NotFound();
            return Ok(arch);
        }
        [HttpPost]
        public async Task<ActionResult<Archive>> CreateArchive([FromBody] Archive arch)
        {
            await archService.Create(arch);
            return CreatedAtAction(nameof(GetArchiveById), new { Id = arch.IdArchive }, arch);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Archive>> UpdateArchive(int id, [FromBody] Archive arch)
        {
            if (arch.IdArchive != id) return BadRequest();
            await archService.Update(arch);
            return Ok(arch);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await archService.Delete(id);
            return NoContent();
        }
    }
}
