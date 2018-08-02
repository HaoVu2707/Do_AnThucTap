using API.Entities;
using API.Infrastructure;
using API.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Services
{
    public class PackageRepository : GenericRepository<PackageEntity, PackageDto, PackageDto>, IPackageRepository
    {
        private DatabaseContext _context;
        private DbSet<PackageEntity> _entity;

        public PackageRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            _entity = _context.Set<PackageEntity>();
        }
        new public async Task<Guid> CreateAsync(PackageDto creationDto)
        {
            var newService = new PackageEntity();

            foreach (PropertyInfo propertyInfo in creationDto.GetType().GetProperties())
            {
                if (newService.GetType().GetProperty(propertyInfo.Name) != null)
                {
                    newService.GetType().GetProperty(propertyInfo.Name).SetValue(newService, propertyInfo.GetValue(creationDto, null));
                }
            }
            
            newService.TotalMoney = creationDto.Amount* creationDto.Price  - creationDto.Discount;

            await _entity.AddAsync(newService);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Database context could not create data.");
            }
            return newService.Id;
        }

       
      
    }
}
