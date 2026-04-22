using ApiDlyaKursovoyTry2.Models;
using ApiDlyaKursovoyTry2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        private readonly IDistributedCache _distributedCache;
        private const string ArchiveByAddress = "DISCP";
        private readonly NormalnayaKursovayaContext _db;
        private readonly ILogger<ArchiveController> _logger;
        public ArchiveController(ILogger<ArchiveController> logger,
                            NormalnayaKursovayaContext context,
                                        IMemoryCache memoryCache,
                                IDistributedCache distributedCache)
        {
            _logger = logger;
            _db = context;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        private Archive[]? GetArchiveByAddressFromDataBase(string address)
        {
            Archive[]? cachedValue = _db.Archives.Where(p => p.HomeAddress == address).ToArray();                    
            DistributedCacheEntryOptions cacheEntryOptions = new()
            {
                SlidingExpiration = TimeSpan.FromSeconds(5),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
            };
            byte[]? cachedValueBytes =
            JsonSerializer.SerializeToUtf8Bytes(cachedValue);

            _distributedCache.Set(ArchiveByAddress,
            cachedValueBytes, cacheEntryOptions);

            return cachedValue;
        }

        [HttpGet]
        [Route("byaddress")]
        [Produces(typeof(Archive[]))]
        public IEnumerable<Archive> GetArchiveByAddress(string address)
        {
            
            byte[]? cachedValueBytes = _distributedCache.Get(ArchiveByAddress);
            Archive[]? cachedValue = null;
            if (cachedValueBytes is null)
            {
                cachedValue = GetArchiveByAddressFromDataBase(address);
            }
            else
            {
                cachedValue = JsonSerializer.Deserialize<Archive[]?>(cachedValueBytes);
                if (cachedValue is null)
                {
                    cachedValue =  GetArchiveByAddressFromDataBase(address);
                }
            }
            return cachedValue ?? Enumerable.Empty<Archive>();
        }



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


        [HttpGet("{name}")]

        public async Task<IEnumerable<Archive>> GetByName(string name)
        {
            if (Random.Shared.Next(1, 4) == 1)

            {
                return await archService.GetByName(name);
            }
            throw new Exception("Randomized fault.");
        }




        [HttpGet("{id:int}")]
        [ResponseCache(Duration = 5,
         Location = ResponseCacheLocation.Any,
         VaryByHeader = "User-Agent"
         )]
        public async ValueTask<Archive?> GetArchiveById(int id)
        {
            return await _db.Archives.FindAsync(id);
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
