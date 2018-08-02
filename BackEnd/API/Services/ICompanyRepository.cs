using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICompanyRepository : IGenericRepository<CompanyEntity, CompanyDto, CompanyForCreationDto>
    {
        new Task<PagedResults<CompanyDto>> GetListAsync(int offset, int limit, string keyword,
            SortOptions<CompanyDto, CompanyEntity> sortOptions,
            FilterOptions<CompanyDto, CompanyEntity> filterOptions,
            IQueryable<CompanyEntity> querySearch
            );

        new Task<Guid> CreateAsync(CompanyForCreationDto companyDto);

        new Task<Guid> EditAsync(Guid id, CompanyForCreationDto companyDto);

    }
}
