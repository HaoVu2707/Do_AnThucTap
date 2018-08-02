using API.Entities;
using API.Infrastructure;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("Companies")]
    [Authorize]
    public class CompanyController : GenericController<CompanyEntity, CompanyDto, CompanyForCreationDto>
    {
        private readonly DatabaseContext _context;
        private readonly ICompanyRepository _genericRepository;
        private readonly DbSet<CompanyEntity> _entity;
        private static readonly HttpClient client = new HttpClient();

      
        public CompanyController(ICompanyRepository genericRepository, DatabaseContext context) : base(genericRepository, context)
        {
            _genericRepository = genericRepository;
            _context = context;
            _entity = _context.Set<CompanyEntity>();

        }
        public async Task<IActionResult> GetListAsync(
         [FromQuery] int offset,
         [FromQuery] int limit,
         [FromQuery] SortOptions<CompanyDto, CompanyEntity> sortOptions,
         [FromQuery] FilterOptions<CompanyDto, CompanyEntity> DemoOptions,
         [FromQuery] string keyword,
         [FromQuery] bool isSoldOut = false
         )
        {
            IQueryable<CompanyEntity> querySearch = _entity;
            if (keyword != null)
            {
                querySearch = _entity.Where(
                x => x.Name.Contains(keyword)
                || x.Address.Contains(keyword) || x.PhoneNumber.Contains(keyword) || x.Code.Contains(keyword)
                );
            }

            var handledData = await _genericRepository.GetListAsync(offset, limit, keyword, sortOptions, DemoOptions, querySearch);
            var items = handledData.Items.ToArray();
            int totalSize = handledData.TotalSize;
            return Ok(new { data = items, totalSize });
        }

        [HttpGet]
        [Route("name")]
        public async Task<PagedResults<CompanyDto>> GeListAsync_name()
        {
            IQueryable<CompanyEntity> query = _entity;
            var totalSize = await query.CountAsync();

            string[] ListCode = await _entity.Select(column => column.Code).ToArrayAsync();
            // return ListCode;
            var listWhere = from a in _entity
                            where a.Name == "chinh"
                            select a;
            List<CompanyDto> returnUserList = new List<CompanyDto>();

            foreach (var x in listWhere)
            {
                var dto = new CompanyDto
                {
                    Id = x.Id,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Name = x.Name
                };
                returnUserList.Add(dto);
            }
            var size = await listWhere.CountAsync();
            return new PagedResults<CompanyDto>
            {
                Items = returnUserList,
                TotalSize = size
            };
        }
        [HttpGet]
        [Route("getListCode")]
        public async Task<string[]> GeListAsync_Code()
        {
            IQueryable<CompanyEntity> query = _entity;
            var totalSize = await query.CountAsync();
            string[] Code = await _entity.Select(column => column.Code).ToArrayAsync();
            return Code;
        }


    }
}
