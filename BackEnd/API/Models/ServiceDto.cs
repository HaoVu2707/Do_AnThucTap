using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ServiceDto : BaseDto
    {
        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Name")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Code")]
        public string Code { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide price")]
        public double Price { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide ServiceCatagoryId")]
        public Guid ServiceCatagoryId { get; set; }

        public Guid BranchId { get; set; }

        public ServiceCatagoryDto ServiceCatagory { get; set; }

        

    }
}
