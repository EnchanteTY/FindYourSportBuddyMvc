using FindYourSportBuddy.BL.Entities;
using FindYourSportBuddy.DataAccess;
using FindYourSportBuddy.DataAccess.DTOs;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace FindYourSportBuddy.UI.Controllers.api
{
    public class FriendRequestController : ApiController
    {
        private AppDbContext _context;

        public FriendRequestController()
        {
            _context = new AppDbContext();
        }

        [HttpPost]
        public IHttpActionResult FriendRequest(FriendDTO dTO)
        {
            var userId = User.Identity.GetUserId();
            var request = new FriendRequest
            {
                RequesterId = userId,
                FriendId = dTO.FriendId,
                IsAccepted = false
            };
            _context.FriendRequests.Add(request);
            _context.SaveChanges();
            return Ok();
        }
    }
}
