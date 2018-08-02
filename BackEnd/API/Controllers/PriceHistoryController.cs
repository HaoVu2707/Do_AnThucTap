using API.Entities;
using API.Infrastructure;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("PriceHistory")]
    [Authorize]
    public class PriceHistoryController : GenericController<PriceHistoryEntity, PriceHistoryDto, PriceHistoryDto>
    {
        private readonly DatabaseContext _context;
        private readonly IGenericRepository<PriceHistoryEntity, PriceHistoryDto, PriceHistoryDto> _genericRepository;
        private readonly DbSet<PriceHistoryEntity> _entity;
        private static readonly HttpClient client = new HttpClient();

      
        public PriceHistoryController(IGenericRepository<PriceHistoryEntity, PriceHistoryDto, PriceHistoryDto> genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<PriceHistoryEntity>();

        }
        public async Task<IActionResult> GetListAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<PriceHistoryDto, PriceHistoryEntity> sortOptions,
         [FromQuery] FilterOptions<PriceHistoryDto, PriceHistoryEntity> DemoOptions,
         [FromQuery] string keyword,
         [FromQuery] bool isSoldOut = false
         )
        {
            IQueryable<PriceHistoryEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => (x.Price).ToString().Contains(keyword)
                
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }
      

    }
}
