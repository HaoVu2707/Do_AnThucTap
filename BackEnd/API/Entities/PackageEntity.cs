using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class PackageEntity : BaseEntity

    {
        [Required]
        public Guid ServiceId { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double TotalMoney { get; set; }
        public Guid BranchId { get; set; }
        public string Status { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }

    }
}