using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindYourSportBuddy.BL.Entities
{
    public class FriendRequest
    {
        [Key]
        [Column(Order = 1)]
        public string RequesterId { get; set; }


        [Key]
        [Column(Order = 2)]
        public string FriendId { get; set; }

        public ApplicationUser Requester { get; set; }

        public ApplicationUser Friend { get; set; }

        public bool IsAccepted { get; set; }
    }
}
