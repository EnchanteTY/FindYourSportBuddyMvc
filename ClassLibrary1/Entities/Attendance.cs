using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindYourSportBuddy.BL.Entities
{
    public class Attendance
    {
        [Key]
        [Column(Order = 1)]
        public int EventId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }

        public virtual Event Event { get; set; }

        public virtual ApplicationUser Attendee { get; set; }


    }
}
