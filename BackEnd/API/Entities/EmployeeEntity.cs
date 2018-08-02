using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public Guid BranchId { get; set; }
        [Required]
        public Guid UserEmployeeId { get; set; }
        [Required]
        public UserEntity UserEmployee { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public  DateTime BirthDay { get; set; }

    }
}
