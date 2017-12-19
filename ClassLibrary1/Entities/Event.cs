using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FindYourSportBuddy.BL.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ApplicationUser Organizer { get; set; }

        public string OrganizerName { get; set; }

        public string OrganizerId { get; set; }

        public DateTime DateTime { get; set; }

        public Region Region { get; set; }

        public string Venue { get; set; }

        public byte CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public bool IsCancelled { get; private set; }

        public int ParticipantsRequired { get; set; }

        public bool IsPrivate { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }


        public Event()
        {
            Attendances = new Collection<Attendance>();

        }

        public void CancelEvent()
        {
            IsCancelled = true;

            var notification = Notification.EventCanceled(this);


            foreach (var participant in Attendances.Select(a => a.Attendee))
            {
                participant.Notify(notification);
            }
        }

        public void EditEvent(string venue, DateTime dateTime, byte categoryId, int participants, Region region, string name, bool isPrivate)
        {

            var notification = Notification.EventEdited(this, DateTime, Venue);


            Venue = venue;
            DateTime = dateTime;
            CategoryId = categoryId;
            ParticipantsRequired = participants;
            Region = region;
            Name = name;
            IsPrivate = isPrivate;

            foreach (var participant in Attendances.Select(a => a.Attendee))
            {

                participant.Notify(notification);

            }

        }

    }
}
