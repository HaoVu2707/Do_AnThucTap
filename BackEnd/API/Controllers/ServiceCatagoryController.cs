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
    [Route("ServiceCategories")]
    [Authorize]
    public class ServiceCatagoryController : GenericController<ServiceCatagoryEntity, ServiceCatagoryDto, ServiceCatagoryForCreationDto>
    {
        private readonly DatabaseContext _context;
        private readonly IServiceCategoryRepository _genericRepository;
        private readonly DbSet<ServiceCatagoryEntity> _entity;
        private static readonly HttpClient client = new HttpClient();

      
        public ServiceCatagoryController(IServiceCategoryRepository genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<ServiceCatagoryEntity>();

        }

        [AllowAnonymous]
        [Route("ListServiceCategoryAll")]
        public async Task<IActionResult> GetAllForCustomerAsync()
        {
            var handledData = await _genericRepository.GetAllAsync();

            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;

            return Ok(new { data = items, totalSize });
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("ListServiceCategory")]
        public async Task<IActionResult> GetListForCustomerAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<ServiceCatagoryDto, ServiceCatagoryEntity> sortOptions,
         [FromQuery] FilterOptions<ServiceCatagoryDto, ServiceCatagoryEntity> DemoOptions,
         [FromQuery] string keyword,
         [FromQuery] bool isSoldOut = false
         )
        {
            IQueryable<ServiceCatagoryEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => x.Name.Contains(keyword)
                || x.Code.Contains(keyword)
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }
        public async Task<IActionResult> GetListForCompanyAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<ServiceCatagoryDto, ServiceCatagoryEntity> sortOptions,
         [FromQuery] FilterOptions<ServiceCatagoryDto, ServiceCatagoryEntity> DemoOptions,
         [FromQuery] string keyword,
         [FromQuery] bool isSoldOut = false
         )
        {
            // lấy service theo chi nhánh , chưa chỉnh sửa
            IQueryable<ServiceCatagoryEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => x.Name.Contains(keyword)
                || x.Code.Contains(keyword)
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }


        [HttpGet]
        [Route("getListCode")]
        public async Task<string[]> GeListAsync_Code()
        {
            IQueryable<ServiceCatagoryEntity> query = _entity;
            var totalSize = await query.CountAsync();
            string[] Code = await _entity.Select(column => column.Code).ToArrayAsync();
            return Code;
        }


    }
}
