using FindYourSportBuddy.BL.Entities;
using FindYourSportBuddy.BL.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FindYourSportBuddy.UI.ViewModels
{
    public class EventFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Region Region { get; set; }

        [Required]
        public string Venue { get; set; }


        [Required]
        [ValidDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Category")]
        public byte CategoryId { get; set; }

        [Required]
        [Display(Name = "Number of Participants Required")]
        public int ParticipantsRequired { get; set; }

        [Display(Name = "Mark event as private")]
        public bool IsPrivate { get; set; }


        public SelectList CategoryList { get; set; }

        public DateTime GetDateTime()
        {

            return DateTime.Parse($"{ Date} {Time}");

        }

        public string Heading { get; set; }

        public string Action { get; set; }
    }
}