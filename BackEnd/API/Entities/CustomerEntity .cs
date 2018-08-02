using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class CustomerEntity : BaseEntity
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public Guid UserCustormerId { get; set; }
        [Required]
        public UserEntity UserCustormer { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }

    }
}
