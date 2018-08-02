using API.Entities;
using API.Helpers;
using API.Infrastructure;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Services
{
    public class OrderRepository : GenericRepository<OrderEntity, OrderDto, OrderForCreationDto>, IOrderRepository
    {
        private DatabaseContext _context;
        private DbSet<OrderEntity> _order;
        private DbSet<PackageEntity> _package;
        private DbSet<SellingServiceEntity> _sellingService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;

        public OrderRepository(DatabaseContext context, UserManager<UserEntity> userManager, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _order = _context.Set<OrderEntity>();
            _package = _context.Set<PackageEntity>();
            _sellingService = context.Set<SellingServiceEntity>();
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<PagedResults<OrderDto>> GetAllAsync()
        {

            IQueryable<OrderEntity> query = _order;
            
            var items = await query
                .ToArrayAsync();

            var totalSize = await query.CountAsync();

            var returnItems = Mapper.Map<List<OrderDto>>(items);



            for (var i = 0; i < items.Length; i++)
            {
                
                returnItems[i].PackageList = new List<PackageDto>();

                var packageIdList = JsonConvert.DeserializeObject<List<Guid>>(items[i].PackageIdList);
                foreach (Guid packageId in packageIdList)
                {
                    var package = await _context.Packages.SingleOrDefaultAsync(s => s.Id == packageId);
                    if (package == null)
                    {
                        throw new Exception("Can not find storage with Id=" + packageId);
                    }
                    var packageDto = Mapper.Map<PackageDto>(package);
                    returnItems[i].PackageList.Add(packageDto);
                }

            }
            return new PagedResults<OrderDto>
            {
                Items = returnItems,
                TotalSize = totalSize
            };

        }
        public async Task<PagedResults<OrderDto>> GetListAsync(
            int offset, int limit, string keyword,
            SortOptions<OrderDto, OrderEntity> sortOptions, 
            FilterOptions<OrderDto, OrderEntity> filterOptions,
            IQueryable<OrderEntity> querySearch)
        {

            IQueryable<OrderEntity> query = _order;
            query = sortOptions.Apply(query);
            query = filterOptions.Apply(query);

            if (keyword != null)
            {
               // DateTime a = DateTime.ParseExact(DateTime.Now.Date.ToString(), "dd/MM/yyyy",
                   // System.Globalization.CultureInfo.InvariantCulture).Date;
                query = query.Where(p =>
                    p.Code.Contains(keyword)
                   || p.Status.Contains(keyword)
                );
               
            }

            var items = await query
                .Skip(offset * limit)
                .Take(limit)
                .ToArrayAsync();

            var totalSize = await query.CountAsync();

            var returnItems = Mapper.Map<List<OrderDto>>(items);



            for (var i = 0; i < items.Length; i++)
            {

                returnItems[i].PackageList = new List<PackageDto>();

                var packageIdList = JsonConvert.DeserializeObject<List<Guid>>(items[i].PackageIdList);
                foreach (Guid packageId in packageIdList)
                {
                    var package = await _context.Packages.SingleOrDefaultAsync(s => s.Id == packageId);
                    if (package == null)
                    {
                        throw new Exception("Can not find storage with Id=" + packageId);
                    }
                    var packageDto = Mapper.Map<PackageDto>(package);
                    returnItems[i].PackageList.Add(packageDto);
                }

            }
            return new PagedResults<OrderDto>
            {
                Items = returnItems,
                TotalSize = totalSize
            };

        }

        new public async Task<Guid> CreateAsync(OrderForCreationDto creationDto)
        {
            var newOrder = new OrderEntity();

            foreach (PropertyInfo propertyInfo in creationDto.GetType().GetProperties())
            {
                if (newOrder.GetType().GetProperty(propertyInfo.Name) != null && propertyInfo.Name != "ServiceList")
                {
                    newOrder.GetType().GetProperty(propertyInfo.Name).SetValue(newOrder, propertyInfo.GetValue(creationDto, null));
                }
            }

            

            double toTalMoneyForOrder = 0, discountForOrder = 0;
            var packageIdList = new List<Guid>();

            var packageListForCreation = new List<PackageEntity>();
            var branchIdList = new List<Guid>();

            // lấy danh sách dịch vụ cùng 1 chi nhanh -> tạo 1 gói package 
            var listServiceForCreationPackage = creationDto.ServiceList;
            for (int i = 0; i < listServiceForCreationPackage.Count(); i++)
            {
                var newpackage = new PackageEntity();
                newpackage.Amount = listServiceForCreationPackage[i].Amount;
                newpackage.BranchId = listServiceForCreationPackage[i].BranchId;
                newpackage.Price = listServiceForCreationPackage[i].Price;
                newpackage.ServiceId = listServiceForCreationPackage[i].Id;
                newpackage.Discount = listServiceForCreationPackage[i].Discount;
                newpackage.TotalMoney = newpackage.Price * newpackage.Amount - newpackage.Discount;
                newpackage.Status = CONSTANT.BOOKED;
                newpackage.CreatedDateTime = DateTime.Now;

                await _package.AddAsync(newpackage);
                var packageShow = newpackage;


                // Update Amount in  SellingService
                var sellingServiceUpdate = await _sellingService.SingleOrDefaultAsync(r => r.ServiceId == newpackage.ServiceId && r.BranchId == newpackage.BranchId);
                if (sellingServiceUpdate == null)
                {
                    throw new InvalidOperationException("this Service doesn't sell.");
                }
                sellingServiceUpdate.SoldAmount += newpackage.Amount;
                sellingServiceUpdate.TotalAmount -= newpackage.Amount;

                _sellingService.Update(sellingServiceUpdate);
                var sellingserviceShow = sellingServiceUpdate;

                    // add packageId -> packageIdList in new Order
                    toTalMoneyForOrder += newpackage.TotalMoney;
                    discountForOrder += newpackage.Discount;
                    packageIdList.Add(newpackage.Id);

                    
            }

            //Lay id cua user hien tai chinh la Customer tao don hang => CustomerId
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            newOrder.CustormerId = user.Id;
            newOrder.Status = CONSTANT.BOOKED;
            newOrder.TotalMoney = toTalMoneyForOrder;
            newOrder.Discount = discountForOrder;
            newOrder.PackageIdList = JsonConvert.SerializeObject(packageIdList);
            newOrder.CreatedDateTime = DateTime.Now;

            // Create mã code
            EnhanceCodeGeneratorHelper<OrderEntity> codeGeneratorHelper = new EnhanceCodeGeneratorHelper<OrderEntity>(_context);
            newOrder.Code = await codeGeneratorHelper.ReturnCode(CONSTANT.BILL_PREFIX, CONSTANT.GENERATED_NUMBER_LENGTH);

            await _order.AddAsync(newOrder);

            var orderShow  = newOrder;
            // Update Amout Selling service


            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Database context could not create data.");
            }
            return newOrder.Id;
        }



        
        public async Task<Boolean> UpdateCanceledStatus(Guid id)
        {
            var order = await _order.SingleOrDefaultAsync(r => r.Id == id);
            if (order == null)
            {
                throw new InvalidOperationException("Can not find object with this Id.");
            }
            else
            {
                if (order.Status == CONSTANT.BOOKED) {

                    order.Status = CONSTANT.CANCELED;
                    _order.Update(order);

                    //update Package
                    var packageIdList = JsonConvert.DeserializeObject<List<Guid>>(order.PackageIdList);
                    foreach (var package in packageIdList)
                    {
                        var packageUpdate = await _package.SingleOrDefaultAsync(r => r.Id == package);
                        if (packageUpdate == null)
                        {
                            throw new InvalidOperationException("this package can not found.");
                        }
                        packageUpdate.Status = CONSTANT.CANCELED;

                        // update sellingService

                            var sellingServiceUpdate = await _sellingService.SingleOrDefaultAsync(r => r.ServiceId == packageUpdate.ServiceId && r.BranchId == packageUpdate.BranchId);
                            if (sellingServiceUpdate == null)
                            {
                                throw new InvalidOperationException("this Service doesn't sell.");
                            }
                            sellingServiceUpdate.SoldAmount -= packageUpdate.Amount;
                            sellingServiceUpdate.TotalAmount += packageUpdate.Amount;
                            _sellingService.Update(sellingServiceUpdate);

                    }

                    

                    var updated = await _context.SaveChangesAsync();
                    if (updated < 1)
                    {
                        throw new InvalidOperationException("Database context could not update Status of this Order.");
                    }
                    return true;
                }
                else { return false; }
            }
            
        }



        public async Task<PagedResults<OrderDto>> GetCustomerOrderAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            IQueryable<OrderEntity> query = _order;
            var entity = query.Where(r => r.CustormerId == user.Id);
            if (entity == null)
            {
                throw new InvalidOperationException("Can not find object with this Id.");
            }
            var items = new List<OrderDto>();
            var totalSize = await entity.CountAsync();
            var orders = await entity.ToArrayAsync();

            var returnItems = Mapper.Map<List<OrderDto>>(orders);
            for (var i = 0; i < totalSize; i++)
            {

                returnItems[i].PackageList = new List<PackageDto>();

                var packageIdList = JsonConvert.DeserializeObject<List<Guid>>(orders[i].PackageIdList);
                foreach (Guid packageId in packageIdList)
                {
                    var package = await _context.Packages.SingleOrDefaultAsync(s => s.Id == packageId);
                    if (package == null)
                    {
                        throw new Exception("Can not find storage with Id=" + packageId);
                    }
                    var packageDto = Mapper.Map<PackageDto>(package);
                    returnItems[i].PackageList.Add(packageDto);
                }

            }
            return new PagedResults<OrderDto>
            {
                Items = returnItems,
                TotalSize = totalSize
            };
            
        }
    }

    
}
