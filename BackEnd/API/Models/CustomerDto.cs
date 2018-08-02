using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CustomerDto:BaseDto
    {
        [Sortable]
        [Filterable]
        [Required(ErrorMessage = "You must provide Code")]
        public string Code { get; set; }

        public Guid UserCustormerId { get; set; }
        
        public UserDto UserCustormer { get; set; }
        [Sortable]
        [Filterable]
        public string Gender { get; set; }
        [Sortable]
        [Filterable]
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }


    }
}
