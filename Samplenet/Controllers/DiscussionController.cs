using FindYourSportBuddy.BL.Abstract;
using FindYourSportBuddy.BL.Entities;
using FindYourSportBuddy.UI.ViewModels;
using System;
using System.Web.Mvc;

namespace FindYourSportBuddy.UI.Controllers
{
    public class DiscussionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscussionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // GET: Discussion
        public ActionResult Discuss(int id)
        {


            var discussion = _unitOfWork.Discussions.GetDiscussionsByEventId(id);

            if (discussion != null)
            {
                var viewModel = new DiscussionViewModel
                {
                    Event = _unitOfWork.Events.GetEventIncludingCategory(id),
                    Discussions = discussion,

                };

                return View(viewModel);
            }


            return View();

        }

        [HttpPost]
        public ActionResult Discuss(DiscussionViewModel viewModel)
        {
            var sportevent = _unitOfWork.Events.GetEventIncludingCategory(viewModel.Id);


            var discussion = new Discussion
            {
                DateTime = DateTime.Now,
                CommentString = viewModel.Discussion.CommentString,
                EventId = sportevent.Id,
                WriterId = User.Identity.Name


            };

            _unitOfWork.Discussions.Add(discussion);
            _unitOfWork.Complete();



            viewModel.Event = sportevent;
            viewModel.Discussions = _unitOfWork.Discussions.GetDiscussionsByEventId(viewModel.Id);


            return View(viewModel);
        }
    }
}