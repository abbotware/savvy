namespace Savvy.ZooKeeper.Services
{
    public interface IUserSession
    {
        long UserId { get; set; }

        bool IsAdmin { get; }

        string? Name { get; }
    }
}