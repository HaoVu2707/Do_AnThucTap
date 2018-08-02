using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IServiceCategoryRepository : IGenericRepository<ServiceCatagoryEntity, ServiceCatagoryDto, ServiceCatagoryForCreationDto>
    {
        new Task<PagedResults<ServiceCatagoryDto>> GetListAsync(int offset, int limit, string keyword,
            SortOptions<ServiceCatagoryDto, ServiceCatagoryEntity> sortOptions,

            FilterOptions<ServiceCatagoryDto, ServiceCatagoryEntity> filterOptions,
            IQueryable<ServiceCatagoryEntity> querySearch
            );

        new Task<Guid> CreateAsync(ServiceCatagoryForCreationDto serviceDto);

        new Task<Guid> EditAsync(Guid id, ServiceCatagoryForCreationDto serviceDto);

    }
}
