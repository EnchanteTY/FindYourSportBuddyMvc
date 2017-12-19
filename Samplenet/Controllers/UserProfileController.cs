using FindYourSportBuddy.BL.Abstract;
using FindYourSportBuddy.BL.Entities;
using FindYourSportBuddy.UI.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindYourSportBuddy.UI.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        // GET: UserProfile
        public ActionResult Index(string userName)
        {
            if (userName == null)
            {
                userName = User.Identity.GetUserName();
            }

            var userProfile = _unitOfWork.UserProfiles.GetUserProfileByUserName(userName);
            if (userProfile != null)
            {
                var viewModel = new UserProfileViewModel
                {
                    UserProfile = userProfile,



                };
                ViewBag.Data = "Edit";

                return View(viewModel);
            }
            return RedirectToAction("Create");

        }

        public PartialViewResult UserProfile(string userName)
        {
            var userId = User.Identity.GetUserId();
            var userProfile = _unitOfWork.UserProfiles.GetUserProfileByUserName(userName);

            if (userProfile != null)
            {
                var category = _unitOfWork.Categories.GetCategoryById(userProfile.InterestedSportId);
                var friendRequest = _unitOfWork.FriendRequests.GetFriendRequestSent(userId, userProfile.UserId);
                var pending = _unitOfWork.FriendRequests.GetPendingRequest(userId, userProfile.UserId);

                var viewModel = new UserProfileViewModel
                {
                    UserProfile = userProfile,
                    Category = category.Name,
                    IsFollowing = _unitOfWork.Followings.CheckIsFollowing(userProfile.UserId, userId),
                    IsFriend = false,
                    FriendRequestSent = false,
                    PendingFriendRequest = false
                };

                if (friendRequest != null)
                {
                    viewModel.IsFriend = friendRequest.IsAccepted;

                    if (!viewModel.IsFriend)
                    {
                        viewModel.FriendRequestSent = true;
                    }
                }

                if (pending != null)
                {
                    var request = _unitOfWork.FriendRequests.GetFriendRequestSent(userProfile.UserId, userId);
                    viewModel.IsFriend = request.IsAccepted;
                    if (!viewModel.IsFriend)
                    {
                        viewModel.PendingFriendRequest = true;
                    }

                }





                return PartialView(viewModel);
            }
            return PartialView("NotBuiltYet");
        }

        public ActionResult Create()
        {


            var viewModel = new UserProfileFormViewModel
            {
                CategoryList = new SelectList(_unitOfWork.Categories.Categories, "Id", "Name"),
                Heading = "Build Your Profile",
                Action = "Create",
                Name = User.Identity.GetUserName(),


            };
            return View("UserProfileForm", viewModel);
        }

        [HttpPost]
        public ActionResult Create(UserProfileFormViewModel viewModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var userProfile = new UserProfile
                {
                    Name = User.Identity.GetUserName(),
                    UserId = User.Identity.GetUserId(),
                    Age = viewModel.Age,
                    Gender = viewModel.Gender,
                    PreferSportRegion = viewModel.PreferSportRegion,
                    PreferSportTime = viewModel.PreferSportTime,
                    PreferSportVenue = viewModel.PreferSportVenue,
                    Introduction = viewModel.Introduction,
                    InterestedSportId = viewModel.InterestedSportId,



                };

                if (ValidateFile(image))
                {
                    userProfile.ImageData = new byte[image.ContentLength];
                    userProfile.ImageMimeType = image.ContentType;
                    image.InputStream.Read(userProfile.ImageData, 0, image.ContentLength);



                    _unitOfWork.UserProfiles.Add(userProfile);
                    _unitOfWork.Complete();


                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");

        }

        public ActionResult Edit(string userName)
        {

            var profile = _unitOfWork.UserProfiles.GetUserProfileByUserName(userName);
            var viewModel = new UserProfileFormViewModel
            {

                Name = User.Identity.Name,
                UserId = User.Identity.GetUserId(),
                CategoryList = new SelectList(_unitOfWork.Categories.Categories, "Id", "Name"),
                Age = profile.Age,
                Gender = profile.Gender,
                InterestedSportId = profile.InterestedSportId,
                Introduction = profile.Introduction,
                PreferSportRegion = profile.PreferSportRegion,
                PreferSportTime = profile.PreferSportTime,
                PreferSportVenue = profile.PreferSportVenue,
                ImageData = profile.ImageData,

                Heading = "Edit Your Profile",
                Action = "Edit"
            };

            return View("UserProfileForm", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(UserProfileFormViewModel viewModel, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var userProfile = _unitOfWork.UserProfiles.GetUserProfileByUserName(userName);
                if (image != null)
                {
                    if (ValidateFile(image))
                    {
                        userProfile.ImageData = new byte[image.ContentLength];
                        userProfile.ImageMimeType = image.ContentType;
                        image.InputStream.Read(userProfile.ImageData, 0, image.ContentLength);

                    }
                }
                if (TryUpdateModel(userProfile, "", new string[] { "Age", "Gender",
                    "InterestedSportId", "Introduction","PreferSportRegion","PreferSportTime","PreferSportVenue","ImageData","ImageMimeType" }))
                {
                    _unitOfWork.Complete();
                }


                return RedirectToAction("Index");


            }

            return RedirectToAction("Edit");
        }

        public ActionResult Match()
        {
            var userName = User.Identity.Name;
            var myProfile = _unitOfWork.UserProfiles.GetUserProfileByUserName(userName);
            var userProfiles = _unitOfWork.UserProfiles.GetMatchProfile(myProfile.Name, myProfile.InterestedSportId, myProfile.PreferSportRegion);
            var viewModel = new UserProfileViewModel();
            if (userProfiles != null)
            {
                viewModel.UserProfiles = userProfiles;
            }

            return View(viewModel);

        }

        public PartialViewResult GetUserEvent(string userId)
        {
            var viewModel = new UserEventViewModel
            {
                EventsCreated = _unitOfWork.Events.GetAllEventsCreatedByUser(userId),
                EventsParticpated = _unitOfWork.Events.GetEventsParticipatedByUser(userId)

            };
            return PartialView(viewModel);
        }

        private bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 2097152) && allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }


        public FileContentResult GetProfileImage(string userName)
        {
            UserProfile profile = _unitOfWork.UserProfiles.GetUserProfileByUserName(userName);
            if (profile != null)
            {
                return File(profile.ImageData, profile.ImageMimeType);
            }
            else
            {
                return null;
            }
        }



    }
}