using FindYourSportBuddy.BL.Entities;
using System.Collections.Generic;

namespace FindYourSportBuddy.UI.ViewModels
{
    public class DiscussionViewModel
    {
        public int Id { get; set; }

        public List<Discussion> Discussions { get; set; }

        public Discussion Discussion { get; set; }

        public string WriterName { get; set; }

        public Event Event { get; set; }
    }
}