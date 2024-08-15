using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NPlan.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string StudentID { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Fullname { get; set; } = string.Empty;

        [Required]
        public string VerificationStatus { get; set; } = string.Empty;

        public int Score { get; set; }

        [Required]
        public string Diploma { get; set; } = string.Empty;

        public ICollection<InterestGroup> InterestGroups { get; set; } = new List<InterestGroup>();

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
    }
}
