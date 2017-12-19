using FindYourSportBuddy.BL.Entities;
using FindYourSportBuddy.DataAccess;
using FindYourSportBuddy.DataAccess.DTOs;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace FindYourSportBuddy.UI.Controllers.api
{
    public class FollowingController : ApiController
    {

        private AppDbContext _context;

        public FollowingController()
        {
            _context = new AppDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDTO dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("Following already exists.");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null)
                return NotFound();

            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
