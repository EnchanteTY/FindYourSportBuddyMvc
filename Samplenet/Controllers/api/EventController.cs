using FindYourSportBuddy.BL.Abstract;
using System.Web.Http;

namespace FindYourSportBuddy.UI.Controllers.api
{
    public class EventController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //logically delete
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userName = User.Identity.Name;
            var sportEvent = _unitOfWork.Events.GetEventIncludingAttendance(id, userName);

            if (sportEvent.IsCancelled)
            {
                return NotFound();
            }

            sportEvent.CancelEvent();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
