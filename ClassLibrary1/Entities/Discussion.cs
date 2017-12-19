using System;
using System.ComponentModel.DataAnnotations;

namespace FindYourSportBuddy.BL.Entities
{
    public class Discussion
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }


        [Required]
        public string WriterId { get; set; }

        [Required]
        public string CommentString { get; set; }

        public DateTime DateTime { get; set; }
    }
}
