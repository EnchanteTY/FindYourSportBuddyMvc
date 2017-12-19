using System;
using System.ComponentModel.DataAnnotations;

namespace FindYourSportBuddy.BL.Entities
{

    public enum NotificationType
    {
        EventCancelled = 1,
        EventEdited = 2,
        ParticipantRegister = 3,
        ParticipantQuit = 4
    }

    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Event Event { get; set; }



        protected Notification()
        {

        }

        private Notification(NotificationType type, Event sportevent)
        {
            if (sportevent == null)
                throw new ArgumentNullException("sportevent");

            Type = type;
            Event = sportevent;
            DateTime = DateTime.Now;

        }

        public static Notification EventCanceled(Event sportevent)
        {
            return new Notification(NotificationType.EventCancelled, sportevent);
        }


        public static Notification EventEdited(Event newEvent, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.EventEdited, newEvent);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

    }


}
