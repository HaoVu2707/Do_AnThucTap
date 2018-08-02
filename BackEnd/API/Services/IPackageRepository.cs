using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPackageRepository : IGenericRepository<PackageEntity, PackageDto, PackageDto>
    {
        new Task<PagedResults<PackageDto>> GetListAsync(int offset, int limit, string keyword,
            SortOptions<PackageDto, PackageEntity> sortOptions,
            FilterOptions<PackageDto, PackageEntity> filterOptions,
            IQueryable<PackageEntity> querySearch
            );

        new Task<Guid> CreateAsync(PackageDto serviceDto);

        new Task<Guid> EditAsync(Guid id, PackageDto serviceDto);

    }
}
