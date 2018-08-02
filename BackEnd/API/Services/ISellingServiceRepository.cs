using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ISellingServiceRepository : IGenericRepository<SellingServiceEntity, SellingServiceDto, SellingServiceForCreationDto>
    {
        new Task<PagedResults<SellingServiceDto>> GetListAsync(int offset, int limit, string keyword,
            SortOptions<SellingServiceDto, SellingServiceEntity> sortOptions,
            FilterOptions<SellingServiceDto, SellingServiceEntity> filterOptions,
            IQueryable<SellingServiceEntity> querySearch
            );

        new Task<Guid> CreateAsync(SellingServiceForCreationDto creationDto);
       // new bool SetStatus ()
       new Task<Guid> EditAsync(Guid id, SellingServiceDto sellingServiceDto);

    }
}
