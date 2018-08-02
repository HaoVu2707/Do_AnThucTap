using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ServiceEntity : BaseEntity

    {
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public string Code { get; set; }
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public Guid BranchId { get; set; }
        [Required]
        public Guid ServiceCatagoryId { get; set; }

        public  ServiceCatagoryEntity ServiceCatagory { get; set; }


       



        }
}