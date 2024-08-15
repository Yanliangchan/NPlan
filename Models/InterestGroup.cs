using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPlan.Models
{
    public class InterestGroup
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(500)]
        public string InterestGroupType { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
