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
    [Route("SellingServices")]
    [Authorize]
    public class SellingServiceController : GenericController<SellingServiceEntity, SellingServiceDto, SellingServiceForCreationDto>
    {
        private readonly DatabaseContext _context;
        private readonly ISellingServiceRepository _genericRepository;
        private readonly DbSet<SellingServiceEntity> _entity;
        private static readonly HttpClient client = new HttpClient();

      
        public SellingServiceController(ISellingServiceRepository genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<SellingServiceEntity>();

        }
        public async Task<IActionResult> GetListAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<SellingServiceDto, SellingServiceEntity> sortOptions,
         [FromQuery] FilterOptions<SellingServiceDto, SellingServiceEntity> DemoOptions,
         [FromQuery] string keyword,
         [FromQuery] bool isSoldOut = false
         )
        {
            IQueryable<SellingServiceEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => x.Code.Contains(keyword)
                || (x.Price).ToString().Contains(keyword) || (x.TotalAmount).ToString().Contains(keyword) || (x.SoldAmount).ToString().Contains(keyword)
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }
        [AllowAnonymous]
        [Route("ListServiceAll")]
        public async Task<IActionResult> GetAllForCustomerAsync()
        {
            var handledData = await _genericRepository.GetAllAsync();

            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;

            return Ok(new { data = items, totalSize });
        }

    }
}
