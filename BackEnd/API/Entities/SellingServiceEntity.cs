using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SellingServiceEntity : BaseEntity

    {
        [Required]
        public string Code { get; set; }
        [Required]
        public Guid ServiceId { get; set; }
        public ServiceEntity Service { get; set; }
        [Required]
        public double SoldAmount { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public Guid BranchId { get; set; }
        public BranchEntity Branch { get; set; }
        [Required]
        public bool IsRunning { get; set; }
        [Required]
        public bool IsDiscountMoney { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        public Guid CreatedUserId { get; set; }

        public UserEntity CreatedUser { get; set; }

    }
}