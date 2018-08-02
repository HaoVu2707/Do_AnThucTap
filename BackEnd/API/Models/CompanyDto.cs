using API.Entities;
using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CompanyDto : BaseDto
    {
        [Sortable]
        [Filterable]
        [Required(ErrorMessage ="You must provide Name")]
        public string Name { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Name")]
        public string PhoneNumber { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Address")]
        public string Address { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Code")]
        public string Code { get; set; }

        public ICollection<BranchEntity> Branchs { get; set; }

    }
}
