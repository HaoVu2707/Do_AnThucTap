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
    public class CompanyRepository : GenericRepository<CompanyEntity, CompanyDto, CompanyForCreationDto>, ICompanyRepository
    {
        private DatabaseContext _context;
        private DbSet<CompanyEntity> _entity;

        public CompanyRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            _entity = _context.Set<CompanyEntity>();
        }
        new public async Task<Guid> CreateAsync(CompanyForCreationDto creationDto)
        {
            var newCompany = new CompanyEntity();

            var existedCompany = _entity.FirstOrDefault(p => p.Code == creationDto.Code);
            if (existedCompany != null)
            {
                throw new InvalidOperationException("Company has Code which is existed on database");
            }

            foreach (PropertyInfo propertyInfo in creationDto.GetType().GetProperties())
            {
                if (newCompany.GetType().GetProperty(propertyInfo.Name) != null)
                {
                    newCompany.GetType().GetProperty(propertyInfo.Name).SetValue(newCompany, propertyInfo.GetValue(creationDto, null));
                }
            }
            
            await _entity.AddAsync(newCompany);


            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Database context could not create data.");
            }
            return newCompany.Id;
        }

        new public async Task<Guid> EditAsync(Guid id, CompanyForCreationDto productDto)
        {
            var entity = await _entity.SingleOrDefaultAsync(r => r.Id == id);
            if (entity == null)
            {
                throw new InvalidOperationException("Can not find object with this Id.");
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
