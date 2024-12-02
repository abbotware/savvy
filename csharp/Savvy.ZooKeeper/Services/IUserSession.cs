namespace Savvy.ZooKeeper.Services
{
    public interface IUserSession
    {
        long UserId { get; set; }

        string? Name { get; }
    }
}