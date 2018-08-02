using API.Entities;
using API.Models;
using AutoMapper;
using System;

namespace API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerEntity, CustomerDto>();
            CreateMap<EmployeeEntity, EmployeeDto>();
            CreateMap<AccessiblePageEntity, AccessiblePageDto>();
            CreateMap<RoleEntity, RoleDto>();
            CreateMap<UserEntity, UserDto>();
            CreateMap<CompanyEntity, CompanyDto>();
            CreateMap<BranchEntity, BranchDto>();
            CreateMap<ServiceCatagoryEntity, ServiceCatagoryDto>();
            CreateMap<ServiceEntity, ServiceDto>();
            CreateMap<OrderEntity, OrderDto>();
            CreateMap<PriceHistoryEntity, PriceHistoryDto>();
            CreateMap<SellingServiceEntity, SellingServiceDto>();
            CreateMap<PackageEntity, PackageDto>();
            CreateMap<char, Guid>();
        }
    }
}
