using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IServiceRepository : IGenericRepository<ServiceEntity, ServiceDto, ServiceForCreationDto>
    {
        new Task<PagedResults<ServiceDto>> GetListAsync(int offset, int limit, string keyword,
            SortOptions<ServiceDto, ServiceEntity> sortOptions,
            FilterOptions<ServiceDto, ServiceEntity> filterOptions,
            IQueryable<ServiceEntity> querySearch
            );

        new Task<Guid> CreateAsync(ServiceForCreationDto serviceDto);

        new Task<Guid> EditAsync(Guid id, ServiceForCreationDto serviceDto);

    }
}
