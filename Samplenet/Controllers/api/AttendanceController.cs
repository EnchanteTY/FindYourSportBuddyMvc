using FindYourSportBuddy.BL.Abstract;
using FindYourSportBuddy.BL.Entities;
using FindYourSportBuddy.DataAccess.DTOs;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace FindYourSportBuddy.UI.Controllers.api
{
    [Authorize]
    public class AttendanceController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Join(AttendanceDTO dTO)
        {
            var userId = User.Identity.GetUserId();
            var currrentAttendanceCount = _unitOfWork.Attendances.GetAttendanceCountById(dTO.EventId);
            if (_unitOfWork.Attendances.CheckAttendance(userId, dTO.EventId))
            {
                return BadRequest("You have already registered for this event.");
            }
            if (!_unitOfWork.Attendances.CheckIfFullyRegistered(currrentAttendanceCount))
            {

                var attendance = new Attendance
                {
                    AttendeeId = userId,
                    EventId = dTO.EventId
                };

                _unitOfWork.Attendances.Add(attendance);
                _unitOfWork.Complete();

            }
            else
            {
                return BadRequest("No more space for new attendances.");
            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {

            var userId = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances.GetAttendance(id, userId);
            if (attendances == null)
            {
                return NotFound();
            }

            _unitOfWork.Attendances.Remove(attendances);
            _unitOfWork.Complete();
            return Ok(id);
        }
    }
}
