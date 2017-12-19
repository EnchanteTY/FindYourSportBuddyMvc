using FindYourSportBuddy.DataAccess;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FindYourSportBuddy.UI.Controllers
{
    public class FriendController : Controller
    {
        private AppDbContext _context;
        public FriendController()
        {
            _context = new AppDbContext();
        }
        // GET: Friend
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var friends = _context.FriendRequests
                .Where(f => f.FriendId == userId && f.IsAccepted)
                .Select(f => f.Requester)
                .ToList();
            var myfriends = _context.FriendRequests
                .Where(f => f.RequesterId == userId && f.IsAccepted)
                .Select(f => f.Friend)
                .ToList();
            var total = friends.Union(myfriends);

            return View(total);
        }

        public PartialViewResult FriendRequests()
        {
            var userId = User.Identity.GetUserId();
            var pending = _context.FriendRequests.Where(f => f.FriendId == userId && !f.IsAccepted)
                .Include(f => f.Requester)
                .ToList();
            return PartialView(pending);
        }


    }
}