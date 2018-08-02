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
    [Route("Branchs")]
    [Authorize]
    public class BranchController : GenericController<BranchEntity, BranchDto, BranchForCreationDto> 
    {
        private readonly DatabaseContext _context;
        private readonly IBranchRepository _genericRepository;
        private readonly DbSet<BranchEntity> _entity;
        private static readonly HttpClient client = new HttpClient();

      
        public BranchController(IBranchRepository genericRepository , DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<BranchEntity>();

        }
        public async Task<IActionResult> GetListAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<BranchDto, BranchEntity> sortOptions,
         [FromQuery] FilterOptions<BranchDto, BranchEntity> DemoOptions,
         [FromQuery] string keyword,
         [FromQuery] bool isSoldOut = false
         )
        {
            IQueryable<BranchEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => x.Name.Contains(keyword)
                || x.Address.Contains(keyword) || x.PhoneNumber.Contains(keyword)
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }
        //[HttpGet]
        //[Route("getListCompanies")]
        //public async Task<string[]> GeListAsync_Code()
        //{
        //    IQueryable<CompanyEntity> query = _context.;
        //    var totalSize = await query.CountAsync();
        //    string[] Code = await _entity.Select(column => column.Code).ToArrayAsync();
        //    return Code;
        //}

    }
}
