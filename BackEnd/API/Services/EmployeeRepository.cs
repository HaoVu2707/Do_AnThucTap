using API.Entities;
using API.Infrastructure;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Services
{
    public class EmployeeRepository 
    {
        private DatabaseContext _context;
        private DbSet<EmployeeEntity> _entity;
        private IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeRepository(DatabaseContext context, UserManager<UserEntity> userManager,
                IHttpContextAccessor httpContextAccessor
         )
        {
            _userManager = userManager;
            _context = context;
            _entity = _context.Set<EmployeeEntity>();
            _httpContextAccessor = httpContextAccessor;

            var config1 = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeEntity, EmployeeDto>();
            });
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserEntity, UserDto>();
            });

            _mapper = config1.CreateMapper();
            _mapper = config.CreateMapper();
        }

    //    public async Task<PagedResults<UserDto>> GetListAsync(int offset, int limit, string keyword,
    //       SortOptions<EmployeeDto, EmployeeEntity> sortOptions, FilterOptions<EmployeeDto, EmployeeEntity> filterOptions,
    //       IQueryable<EmployeeEntity> querySearch
    //       )
    //    {
    //        IQueryable<EmployeeEntity> query = _entity;
    //        query = sortOptions.Apply(query);
    //        query = filterOptions.Apply(query);
    //        if (keyword != null)
    //        {
    //            query = querySearch;
    //        }

    //        var size = await query.CountAsync();

    //        var items = await query
    //            .Skip(offset * limit)
    //            .Take(limit)
    //            .ToArrayAsync();

    //        List<EmployeeDto> returnUserList = new List<EmployeeDto>();

    //        foreach (EmployeeEntity userEmployee in items)

    //        {
    //            var x = userEmployee.UserEmployee;

    //            var roleNames = await _userManager.GetRolesAsync(x);
    //            var branch = _context.FindAsync<Branch>
                
    //            var employeeDto = new EmployeeDto
    //            {
    //                F = userEmployee.FullName,
    //                Email = user.Email,
    //                Address = user.Address,
    //                Gender = user.Gender,
    //                PhoneNumber = user.PhoneNumber,
    //                Id = user.Id,
    //                UserName = user.UserName,
    //                IsActive = user.IsActive,
    //                RoleNames = roleNames
    //            };
    //            returnUserList.Add(userDto);
    //        }

    //        return new PagedResults<UserDto>
    //        {
    //            Items = returnUserList,
    //            TotalSize = size
    //        };

    //    }

    //    public async Task<Guid> CreateAsync(UserForCreationDto creationDto)
    //    {

    //        UserEntity newUser = Activator.CreateInstance<UserEntity>();
    //        newUser.IsActive = true;

    //        foreach (PropertyInfo propertyInfo in creationDto.GetType().GetProperties())
    //        {
    //            if (newUser.GetType().GetProperty(propertyInfo.Name) != null)
    //            {
    //                newUser.GetType().GetProperty(propertyInfo.Name).SetValue(newUser, propertyInfo.GetValue(creationDto, null));
    //            }

    //        }

    //        newUser.UserName = creationDto.Email;

    //        await _userManager.CreateAsync(newUser, creationDto.Password);

    //        foreach (string roleName in creationDto.RoleNames)
    //        {
    //            await _userManager.AddToRoleAsync(newUser, roleName);
    //        }

    //        await _userManager.UpdateAsync(newUser);

    //        return newUser.Id;
    //    }

    //    public async Task<Guid> UpdateAsync(Guid id, UserForUpdationDto updationDto)
    //    {
    //        var user = await _entity.SingleOrDefaultAsync(r => r.Id == id);

    //        if (user == null)
    //        {
    //            throw new InvalidOperationException("Can not find object with this Id.");
    //        }
    //        foreach (PropertyInfo propertyInfo in updationDto.GetType().GetProperties())
    //        {
    //            string key = propertyInfo.Name;
    //            if (key != "Id" && user.GetType().GetProperty(key) != null)
    //            {
    //                user.GetType().GetProperty(key).SetValue(user, propertyInfo.GetValue(updationDto, null));
    //            }
    //        }
    //        user.UserName = updationDto.Email;

    //        var roles = await _userManager.GetRolesAsync(user);
    //        await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
    //        await _userManager.AddToRolesAsync(user, updationDto.RoleNames);

    //        _entity.Update(user);
    //        var updated = await _context.SaveChangesAsync();
    //        if (updated < 1)
    //        {
    //            throw new InvalidOperationException("Database context could not update data.");
    //        }
    //        return id;
    //    }

    //    public async Task<Guid> ChangeStatus(Guid id, bool status)
    //    {
    //        var user = await _entity.FirstOrDefaultAsync(p => p.Id == id);
    //        if (user == null)
    //        {
    //            throw new Exception("Can not find user with this id");
    //        }
    //        user.IsActive = status;
    //        _entity.Update(user);

    //        var updated = await _context.SaveChangesAsync();
    //        if (updated < 1)
    //        {
    //            throw new InvalidOperationException("Database context could not update data.");
    //        }
    //        return id;

    //    }

    //    public async Task<UserDto> GetCurrentUser()
    //    {
    //        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
    //        var roleNames = await _userManager.GetRolesAsync(user);
    //        var userDto = _mapper.Map<UserDto>(user);
    //        userDto.RoleNames = roleNames;
    //        return userDto;
    //    }

    //    public async Task<Guid> ResetPassword(Guid id, UserForResetPasswordDto resetPasswordDto)
    //    {
    //        var user = await _entity.SingleOrDefaultAsync(r => r.Id == id);

    //        if (user == null)
    //        {
    //            throw new InvalidOperationException("Can not find object with this Id.");
    //        }
    //        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
    //        await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDto.Password);

    //        _entity.Update(user);
    //        var updated = await _context.SaveChangesAsync();
    //        if (updated < 1)
    //        {
    //            throw new InvalidOperationException("Database context could not update data.");
    //        }
    //        return id;
    //    }

    //    public Task<PagedResults<EmployeeEntity>> GetListAsync(int offset, int limit, string keyword, SortOptions<EmployeeDto, EmployeeEntity> sortOptions, FilterOptions<EmployeeDto, EmployeeEntity> filterOptions, IQueryable<EmployeeEntity> querySearch)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Guid> CreateAsync(EmployeeDto creationDto)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Guid> UpdateAsync(Guid id, EmployeeDto creationDto)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    Task<EmployeeDto> IEmployeeRepository.GetCurrentUser()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Guid> ResetPassword(Guid id, EmployeeDto resetPasswordDto)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<PagedResults<UserDto>> GetListAsync(int offset, int limit, string keyword, SortOptions<UserDto, UserEntity> sortOptions, FilterOptions<UserDto, UserEntity> filterOptions, IQueryable<UserEntity> querySearch)
    //    {
    //        throw new NotImplementedException();
    //    }
    }
}
