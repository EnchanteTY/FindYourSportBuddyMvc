using FindYourSportBuddy.BL.Abstract;
using FindYourSportBuddy.DataAccess.DTOs;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FindYourSportBuddy.UI.Controllers.api
{
    [Authorize]
    public class NotificationController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;



        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDTO> GetNotification()
        {
            var userId = User.Identity.GetUserId();
            //check if null

            var notifications = _unitOfWork.Notifications.GetUnreadNotificationWithEvent(userId);

            //MAPPING FROM NOTIFICATION TO NOTIFICATION DTO
            return notifications.Select(n => new NotificationDTO()
            {
                DateTime = n.DateTime,
                Event = new EventDTO
                {
                    Organizer = new UserDTO
                    {
                        Name = n.Event.OrganizerName

                    },

                    DateTime = n.Event.DateTime,
                    Id = n.Event.Id,
                    Name = n.Event.Name,
                    IsCancelled = n.Event.IsCancelled,
                    Venue = n.Event.Venue,
                },
                OriginalDateTime = n.OriginalDateTime,
                OriginalVenue = n.OriginalVenue,
                Type = n.Type
            });

        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.UserNotifications.GetUnreadUserNotification(userId);

            notifications.ForEach(n => n.Read());
            _unitOfWork.Complete();

            return Ok();

        }


    }
}
