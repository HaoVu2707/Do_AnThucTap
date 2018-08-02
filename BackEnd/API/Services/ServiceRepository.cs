using API.Entities;
using API.Infrastructure;
using API.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Services
{
    public class ServiceRepository : GenericRepository<ServiceEntity, ServiceDto, ServiceForCreationDto>, IServiceRepository
    {
        private DatabaseContext _context;
        private DbSet<ServiceEntity> _entity;

        public ServiceRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            _entity = _context.Set<ServiceEntity>();
        }
        new public async Task<Guid> CreateAsync(ServiceForCreationDto creationDto)
        {
            var newService = new ServiceEntity();

            var existedCompany = _entity.FirstOrDefault(p => p.Code == creationDto.Code);
            if (existedCompany != null)
            {
                throw new InvalidOperationException("Service has Code which is existed on database");
            }

            foreach (PropertyInfo propertyInfo in creationDto.GetType().GetProperties())
            {
                if (newService.GetType().GetProperty(propertyInfo.Name) != null)
                {
                    newService.GetType().GetProperty(propertyInfo.Name).SetValue(newService, propertyInfo.GetValue(creationDto, null));
                }
            }

            await _entity.AddAsync(newService);


           
            // create items to PriceHistory
            var newHistory = new PriceHistoryEntity
            {
                BranchId = creationDto.BranchId,
                Price = creationDto.Price,
                UpdatedDate = DateTime.Now,
                ServiceId = newService.Id
            };
            await _context.PriceHistory.AddAsync(newHistory);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Database context could not create data.");
            }
            return newService.Id;
        }

        new public async Task<Guid> EditAsync(Guid id, ServiceForCreationDto productDto)
        {
            var entity = await _entity.SingleOrDefaultAsync(r => r.Id == id);
            if (entity == null)
            {
                throw new InvalidOperationException("Can not find object with this Id.");
            }
            if (entity.Price != productDto.Price)
            {
                var newHistory = new PriceHistoryEntity
                {
                    BranchId = productDto.BranchId,
                    Price = productDto.Price,
                    UpdatedDate = DateTime.Now,
                    ServiceId = entity.Id
                };
                await _context.PriceHistory.AddAsync(newHistory);

            }

            // to update DriverGroupDrivers by delete old data and create new data

            foreach (PropertyInfo propertyInfo in productDto.GetType().GetProperties())
            {
                string key = propertyInfo.Name;
                if (key != "Id" && entity.GetType().GetProperty(propertyInfo.Name) != null)
                {
                    entity.GetType().GetProperty(key).SetValue(entity, propertyInfo.GetValue(productDto, null));
                }
            }
            // update into PriceHistory
            

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
