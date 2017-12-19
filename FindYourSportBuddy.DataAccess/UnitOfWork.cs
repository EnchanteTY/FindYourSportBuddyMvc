using FindYourSportBuddy.BL.Abstract;
using FindYourSportBuddy.BL.Abstract.Repositories;
using FindYourSportBuddy.DataAccess.Repositories;

namespace FindYourSportBuddy.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IEventRepository Events { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IUserProfileRepository UserProfiles { get; private set; }
        public IDiscussionRepository Discussions { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IFollowingRespository Followings { get; private set; }
        public IFriendRequestRepository FriendRequests { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Events = new EventRepository(context);
            Attendances = new AttendanceRepository(context);
            Categories = new CategoryRepository(context);
            UserProfiles = new UserProfileRepository(context);
            Discussions = new DiscussionRepository(context);
            UserNotifications = new UserNotificationRepository(context);
            Notifications = new NotificationRepository(context);
            Followings = new FollowingRepository(context);
            FriendRequests = new FriendRequestRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
