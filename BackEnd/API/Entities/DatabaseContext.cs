using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class DatabaseContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<AccessiblePageEntity> AccessiblePages { get; set; }
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<BranchEntity> Branchs { get; set; }
        public DbSet<ServiceCatagoryEntity> ServiceCatagories { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PriceHistoryEntity> PriceHistory { get; set; }
        public DbSet<SellingServiceEntity> SellingServices { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

     
}
}
