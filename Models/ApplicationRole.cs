using Microsoft.AspNetCore.Identity;
using System;

namespace NPlan.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public ApplicationRole() : base()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
            CreatedDate = DateTime.UtcNow;
        }

        public ApplicationRole(string roleName, string description) : base(roleName)
        {
            Description = description;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
