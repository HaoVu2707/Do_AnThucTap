using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PackageDto : BaseDto
    {
        [Sortable]
        [Filterable]
        public double Discount { get; set; }

        [Sortable]
        [Filterable]
        public double TotalMoney { get; set; }

        public Guid BranchId { get; set; }

        [Sortable]
        [Filterable]
        public string Status { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }

        public Guid ServiceId { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }


    }
}
