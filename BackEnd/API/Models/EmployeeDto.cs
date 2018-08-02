using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class EmployeeDto : BaseDto
    {
        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Code")]
        public string Code { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide BranchId")]
        public Guid BranchId { get; set; }

        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide JobTitle")]
        public string JobTitle { get; set; }


        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide UserEmployee")]

        public Guid UserEmployeeId { get; set; }
        public UserDto UserEmployee { get; set; }
        [Sortable]
        [Filterable]
        public string Gender { get; set; }
        [Sortable]
        [Filterable]
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }


    }
}
