using API.Entities;
using API.Infrastructure;
using API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IEmployeeRepository : IUserRepository
    {
        Task<PagedResults<EmployeeEntity>> GetListAsync(int offset, int limit, string keyword,
        SortOptions<EmployeeDto, EmployeeEntity> sortOptions, FilterOptions<EmployeeDto, EmployeeEntity> filterOptions,
        IQueryable<EmployeeEntity> querySearch
        );

        Task<Guid> CreateAsync(EmployeeDto creationDto);

        Task<Guid> UpdateAsync(Guid id, EmployeeDto creationDto);

        Task<Guid> ChangeStatus(Guid id, bool status);

        Task<EmployeeDto> GetCurrentUser();

        Task<Guid> ResetPassword(Guid id, EmployeeDto resetPasswordDto);

    }
}
