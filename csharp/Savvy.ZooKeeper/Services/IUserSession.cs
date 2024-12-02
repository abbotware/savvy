namespace Savvy.ZooKeeper.Services
{
    public interface IUserSession
    {
        long UserId { get; set; }

        bool IsAdmin { get; }

        bool ViewPII { get; }

        string? Name { get; }
    }
}