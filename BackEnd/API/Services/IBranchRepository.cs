using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IBranchRepository : IGenericRepository<BranchEntity, BranchDto, BranchForCreationDto>
    {
        new Task<PagedResults<BranchDto>> GetListAsync(int offset, int limit, string keyword,
            SortOptions<BranchDto, BranchEntity> sortOptions,
            FilterOptions<BranchDto, BranchEntity> filterOptions,
            IQueryable<BranchEntity> querySearch
            );

        new Task<Guid> CreateAsync(BranchForCreationDto companyDto);

        new Task<Guid> EditAsync(Guid id, BranchForCreationDto companyDto);

    }
}
