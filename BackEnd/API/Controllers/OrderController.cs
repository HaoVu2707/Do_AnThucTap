using API.Entities;
using API.Infrastructure;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("Orders")]
    [Authorize]
    public class OrderController : GenericController<OrderEntity, OrderDto, OrderForCreationDto>
    {
        private readonly DatabaseContext _context;
        private readonly IOrderRepository _genericRepository;
        private readonly DbSet<OrderEntity> _entity;
        private static readonly HttpClient client = new HttpClient();


        public OrderController(IOrderRepository genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<OrderEntity>();

        }
        public async Task<IActionResult> GetListAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<OrderDto, OrderEntity> sortOptions,
         [FromQuery] FilterOptions<OrderDto, OrderEntity> DemoOptions,
         [FromQuery] string keyword,
         [FromQuery] bool isSoldOut = false
         )
        {
            IQueryable<OrderEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => x.Code.Contains(keyword)
                || (x.TotalMoney).ToString().Contains(keyword) || (x.Discount).ToString().Contains(keyword)
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }

        [HttpGet("GetListMyOrder")]
        public async Task<IActionResult> GetListOrderForCustomerAsync()
        {
            try
            {
                var handledData = await _genericRepository.GetCustomerOrderAsync();
                var items = handledData.Items.ToArray();
                int totalSize = handledData.TotalSize;
                return Ok(new { data = items, totalSize });
            }
            catch(Exception ex) { return BadRequest(new ExceptionResponse(ex.Message)); }

        }

        [HttpPut("Canceled/{id}")]
        public async Task<IActionResult> ChangeActiveStatus(Guid id)
        {
            try
            {
                var returnId = await _genericRepository.UpdateCanceledStatus(id);
                return Ok(new { id = returnId });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponse(ex.Message));
            }
        }

    }
}
