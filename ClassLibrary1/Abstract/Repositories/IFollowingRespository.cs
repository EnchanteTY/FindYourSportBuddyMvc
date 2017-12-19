namespace FindYourSportBuddy.BL.Abstract.Repositories
{
    public interface IFollowingRespository
    {
        bool CheckIsFollowing(string followeeId, string userId);
    }
}
