using FindYourSportBuddy.BL.Abstract.Repositories;

namespace FindYourSportBuddy.BL.Abstract
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; }
        IAttendanceRepository Attendances { get; }
        ICategoryRepository Categories { get; }
        IUserProfileRepository UserProfiles { get; }
        IDiscussionRepository Discussions { get; }
        IUserNotificationRepository UserNotifications { get; }
        INotificationRepository Notifications { get; }
        IFollowingRespository Followings { get; }
        IFriendRequestRepository FriendRequests { get; }
        void Complete();
    }
}
