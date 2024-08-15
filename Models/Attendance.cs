using System;
using System.ComponentModel.DataAnnotations;

namespace NPlan.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }
    }
}
