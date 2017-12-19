using FindYourSportBuddy.BL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FindYourSportBuddy.UI.ViewModels
{
    public class UserProfileFormViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public byte Age { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Prefer Sport Region")]
        public Region PreferSportRegion { get; set; }


        [Display(Name = "Prefer Sport Venue")]
        public string PreferSportVenue { get; set; }


        [Display(Name = "Prefer Sport Time")]
        public string PreferSportTime { get; set; }

        [Display(Name = "Interested Sport")]
        public byte InterestedSportId { get; set; }

        public SelectList CategoryList { get; set; }

        public string Introduction { get; set; }

        [Display(Name = "Profile Image")]
        public byte[] ImageData { get; set; }


        public string Action { get; set; }

        public string Heading { get; set; }
    }
}