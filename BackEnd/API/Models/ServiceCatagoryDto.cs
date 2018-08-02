using API.Entities;
using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ServiceCatagoryDto : BaseDto
    {
        [Sortable]
        [Filterable]
        [Required(ErrorMessage ="You must provide Name")]
        public string Name { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Code")]
        public string Code { get; set; }

        public ICollection<ServiceEntity> Services { get; set; }

    }
}
