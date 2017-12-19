using FindYourSportBuddy.DataAccess;
using FindYourSportBuddy.DataAccess.DTOs;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace FindYourSportBuddy.UI.Controllers.api
{
    public class FriendController : ApiController
    {
        private AppDbContext _context;

        public FriendController()
        {
            _context = new AppDbContext();
        }


        [HttpPost]
        public IHttpActionResult AcceptRequest(FriendDTO dTO)
        {
            var userId = User.Identity.GetUserId();
            var request = _context.FriendRequests.SingleOrDefault(r => r.RequesterId == dTO.RequesterId && r.FriendId == userId);
            request.IsAccepted = true;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfriend(string id)
        {
            var userId = User.Identity.GetUserId();
            var request = _context.FriendRequests.SingleOrDefault(r => r.RequesterId == userId && r.FriendId == id);
            var acceptance = _context.FriendRequests.SingleOrDefault(r => r.RequesterId == id && r.FriendId == userId);
            if (request != null)
            {
                _context.FriendRequests.Remove(request);
            }
            if (acceptance != null)
            {
                _context.FriendRequests.Remove(acceptance);
            }


            _context.SaveChanges();
            return Ok(id);
        }
    }
}
