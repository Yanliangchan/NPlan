using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPlan.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDateTime { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public int Points { get; set; }

        [Required]
        public int InterestGroupId { get; set; }

        [Required]
        public InterestGroup InterestGroup { get; set; }

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
    }
}
