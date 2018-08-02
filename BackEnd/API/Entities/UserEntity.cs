using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace API.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public bool IsActive { get; set; }

    }
}
