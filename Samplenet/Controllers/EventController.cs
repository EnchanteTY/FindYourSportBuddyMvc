using FindYourSportBuddy.BL.Abstract;
using FindYourSportBuddy.BL.Entities;
using FindYourSportBuddy.DataAccess.DTOs;
using FindYourSportBuddy.UI.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace FindYourSportBuddy.UI.Controllers
{
    [Authorize]
    public class EventController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // GET: Event
        [AllowAnonymous]
        public ActionResult Index(AttendanceDTO attendanceDTO, string query = null)

        {
            var currentAttendances = _unitOfWork.Attendances.GetAttendanceCountById(attendanceDTO.EventId);

            var upcomingEvents = _unitOfWork.Events.GetUpcomingEvents(currentAttendances, query);

            var userId = User.Identity.GetUserId();

            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.EventId);

            var friends = _unitOfWork.FriendRequests.GetAllFriends(userId).ToLookup(f => f.UserName);



            var viewModel = new EventViewModel
            {
                Events = upcomingEvents,
                Heading = "All Upcoming Events",
                ShowJoinButton = User.Identity.IsAuthenticated,
                SearchTerm = query,
                currentAttendanceCount = currentAttendances,
                Attendances = attendances,
                Friends = friends

            };


            return View("EventList", viewModel);
        }

        public ActionResult Search(EventViewModel viewModel)
        {
            return RedirectToAction("Index", new { query = viewModel.SearchTerm });
        }

        public ActionResult Mine()
        {
            var userName = User.Identity.Name;

            var viewModel = new EventViewModel
            {
                Events = _unitOfWork.Events.GetUpcomingEventsByUserName(userName).ToList(),
                Heading = "Events I created",

            };
            return View(viewModel);
        }

        public ActionResult Joining(AttendanceDTO attendanceDTO)
        {
            var userId = User.Identity.GetUserId();
            var participatedEvents = _unitOfWork.Attendances.GetParticipatedEvents(userId);

            var currentAttendances = _unitOfWork.Attendances.GetAttendanceCountById(attendanceDTO.EventId);

            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.EventId);


            var friends = _unitOfWork.FriendRequests.GetAllFriends(userId).ToLookup(f => f.UserName);

            var viewModel = new EventViewModel
            {
                Events = participatedEvents,
                Heading = "Events I Registered",
                ShowJoinButton = User.Identity.IsAuthenticated,
                currentAttendanceCount = currentAttendances,
                Attendances = attendances,
                Friends = friends
            };
            return View("EventList", viewModel);
        }


        public ActionResult Create()
        {

            var viewModel = new EventFormViewModel
            {
                CategoryList = new SelectList(_unitOfWork.Categories.Categories, "Id", "Name"),
                Heading = "Create a New Event",
                Action = "Create"
            };
            return View("EventForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.CategoryList = new SelectList(_unitOfWork.Categories.Categories, "Id", "Name");
                viewModel.Action = "Create";
                return View("EventForm", viewModel);
            }

            var sportEvent = new Event
            {

                OrganizerName = User.Identity.Name,
                OrganizerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.CategoryId,
                Name = viewModel.Name,
                Venue = viewModel.Venue,
                Region = viewModel.Region,
                ParticipantsRequired = viewModel.ParticipantsRequired,
                IsPrivate = viewModel.IsPrivate

            };

            _unitOfWork.Events.Add(sportEvent);
            _unitOfWork.Complete();

            return RedirectToAction("Mine");
        }

        public ActionResult Edit(int id)
        {
            var userName = User.Identity.Name;
            var sportevents = _unitOfWork.Events.GetEventByIdAndUserName(id, userName);

            var viewModel = new EventFormViewModel
            {
                Heading = "Edit this event",
                Action = "Edit",
                CategoryList = new SelectList(_unitOfWork.Categories.Categories, "Id", "Name"),
                Id = sportevents.Id,
                Name = sportevents.Name,
                CategoryId = sportevents.CategoryId,
                Date = sportevents.DateTime.ToString("d MMM yyyy"),
                Time = sportevents.DateTime.ToString("HH:mm"),
                Venue = sportevents.Venue,
                ParticipantsRequired = sportevents.ParticipantsRequired,
                IsPrivate = sportevents.IsPrivate




            };

            return View("EventForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.CategoryList = new SelectList(_unitOfWork.Categories.Categories, "Id", "Name");
                viewModel.Action = "Edit";
                return View("EventForm", viewModel);
            }

            var userName = User.Identity.Name;
            var sportEvent = _unitOfWork.Events.GetEventIncludingAttendance(viewModel.Id, userName);

            sportEvent.EditEvent(viewModel.Venue, viewModel.GetDateTime(),
                viewModel.CategoryId, viewModel.ParticipantsRequired, viewModel.Region, viewModel.Name, viewModel.IsPrivate);

            _unitOfWork.Complete();


            return RedirectToAction("Mine");

        }

        public ActionResult Details(int id)
        {

            var sportevent = _unitOfWork.Events.GetEventIncludingCategory(id);

            if (sportevent == null)
                return HttpNotFound();

            var currentAttendances = _unitOfWork.Attendances.GetAttendanceCountById(id);



            var viewModel = new EventDetailViewModel
            {
                UpcomingEvent = sportevent,
                CurrentAttendances = currentAttendances
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsJoining = _unitOfWork.Attendances.CheckAttendance(userId, id);
                viewModel.IsFriend = _unitOfWork.FriendRequests.CheckIfTwoAreFriends(userId, sportevent.OrganizerId);
            }



            return View("Details", viewModel);
        }






    }

}