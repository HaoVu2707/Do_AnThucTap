using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class PriceHistoryEntity : BaseEntity

    {
        [Required]
        public Guid ServiceId { get; set; }

        public Guid BranchId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }


        

        }
}