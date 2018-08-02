using API.Entities;
using API.Infrastructure;
using API.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Services
{
    public class SellingServiceRepository : GenericRepository<SellingServiceEntity, SellingServiceDto, SellingServiceForCreationDto>, ISellingServiceRepository
    {
        private const double EntrysoldAmount = 0;
        private DatabaseContext _context;
        private DbSet<SellingServiceEntity> _entity;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SellingServiceRepository(DatabaseContext context, UserManager<UserEntity> userManager, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _entity = _context.Set<SellingServiceEntity>();
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        new public async Task<Guid> CreateAsync(SellingServiceForCreationDto creationDto)
        {
            var newCompany = new SellingServiceEntity();


            var existedCompany = _entity.FirstOrDefault(p => p.Code == creationDto.Code);
            if (existedCompany != null)
            {
                throw new InvalidOperationException("SellingService has Code which is existed on database");
            }

            foreach (PropertyInfo propertyInfo in creationDto.GetType().GetProperties())
            {
                if (newCompany.GetType().GetProperty(propertyInfo.Name) != null )
                {
                    newCompany.GetType().GetProperty(propertyInfo.Name).SetValue(newCompany, propertyInfo.GetValue(creationDto, null));
                }
            }
            // gán  Usercreation
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            newCompany.CreatedUserId = user.Id;

            newCompany.SoldAmount = EntrysoldAmount;
            if(creationDto.FromDate > DateTime.Now)
            {
                newCompany.IsRunning = false;
            }
            else if(creationDto.FromDate <= DateTime.Now)
            {
                newCompany.IsRunning = true;
            }

            await _entity.AddAsync(newCompany);


            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Database context could not create data.");
            }
            return newCompany.Id;
        }


        public async Task<Guid> EditAsync(Guid id, SellingServiceDto sellingServiceDto)
        {
            var entity = await _entity.SingleOrDefaultAsync(r => r.Id == id);
            if (entity == null)
            {
                throw new InvalidOperationException("Can not find object with this Id.");
            }

            // to update DriverGroupDrivers by delete old data and create new data


            foreach (PropertyInfo propertyInfo in sellingServiceDto.GetType().GetProperties())
            {
                string key = propertyInfo.Name;
                if (key != "Id" && entity.GetType().GetProperty(propertyInfo.Name) != null)
                {
                    entity.GetType().GetProperty(key).SetValue(entity, propertyInfo.GetValue(sellingServiceDto, null));
                }
            }
            if(entity.TotalAmount == 0)
            {
                entity.IsRunning = false;
            }
            if (entity.TotalAmount != 0 && entity.FromDate < DateTime.Now)
            {
                entity.IsRunning = true;
            }

            _entity.Update(entity);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException("Database context could not update data.");
            }
            return id;
        }


    }
}
