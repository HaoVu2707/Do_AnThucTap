using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OrderDto : CodeBaseDto
    {
       
        [Sortable]
        [Filterable]
        public double Discount { get; set; }

        [Sortable]
        [Filterable]
        public double TotalMoney { get; set; }

        [Sortable]
        [Filterable]
        public Guid CustormerId { get; set; }

        [Sortable]
        [Filterable]
        public string Status { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }

        public List<PackageDto> PackageList { get; set; }


    }
}
