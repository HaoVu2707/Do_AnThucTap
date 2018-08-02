using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PriceHistoryDto : BaseDto
    {

        
        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide ServiceId")]
        public Guid ServiceId { get; set; }

        public Guid BranchId { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide price")]
        public double Price { get; set; }


    }
}
