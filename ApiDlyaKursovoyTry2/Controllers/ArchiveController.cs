using ApiDlyaKursovoyTry2.Models;
using ApiDlyaKursovoyTry2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIdlyaKursovoy.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]

    public class ArchiveController : ControllerBase
    {
        private readonly ArchiveService archService;
        private readonly IMemoryCache _memoryCache;
        private const string OutOfStockProductsKey = "OOSP";

        public ArchiveController(ArchiveService service, IMemoryCache memoryCache)
        {
            this.archService = service;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archive>>> GetAllArchives()
        {
            if (!_memoryCache.TryGetValue(OutOfStockProductsKey,
            out Archive[]? cachedValue))
            {
                cachedValue = (await archService.GetAll()).ToArray();
                MemoryCacheEntryOptions cacheEntryOptions = new()

                {

                    SlidingExpiration = TimeSpan.FromSeconds(5),

                    Size = cachedValue?.Length

                };
                _memoryCache.Set(OutOfStockProductsKey, cachedValue, cacheEntryOptions);
            }
            MemoryCacheStatistics? stats = _memoryCache.GetCurrentStatistics();

            return  Ok(cachedValue ?? Enumerable.Empty<Archive>());
        }



        [HttpGet("{id}")]
        [ResponseCache(Duration = 5, 
        Location = ResponseCacheLocation.Any, 
        VaryByHeader = "User-Agent" 
        )]
        public async Task<ActionResult<Archive>> GetArchiveById(int id)
        {
            var arch = await archService.GetById(id);
            if (arch == null) return NotFound();
            return Ok(arch);
        }

        [HttpGet("{name}")]

        public async Task<IEnumerable<Archive>> GetByName(string name)
        {
            if (Random.Shared.Next(1, 4) == 1)

            {
                return await archService.GetByName(name);
            }
            throw new Exception("Randomized fault.");
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
