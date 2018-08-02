using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SellingServiceDto : BaseDto
    {
        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Code")]
        public string Code { get; set; }

        [Sortable]
        [Filterable]
        public double Discount { get; set; }

        [Sortable]
        [Filterable]
        public double SoldAmount { get; set; }

        [Sortable]
        [Filterable]
        public double TotalAmount { get; set; }

        [Sortable]
        [Filterable]
        public double Price { get; set; }

        public Guid ServiceId { get; set; }

        public ServiceDto Service { get; set; }
        public Guid BranchId { get; set; }
        public BranchDto Branch { get; set; }

        public bool IsRunning { get; set; }
        
        public bool IsDiscountMoney { get; set; }
        
        public DateTime FromDate { get; set; }
        
        public DateTime ToDate { get; set; }

        public Guid CreatedUserId { get; set; }

        public UserDto CreatedUser { get; set; }


    }
}
