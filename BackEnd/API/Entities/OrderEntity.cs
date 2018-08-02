using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class OrderEntity : CodeBaseEntity

    {
        [Required]
        public string PackageIdList { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double TotalMoney { get; set; }
        [Required]
        public Guid CustormerId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }

    }
}