namespace Savvy.ZooKeeper.Services
{
    using Savvy.ZooKeeper.Models;

    public class UserSession : IUserSession
    {
        private readonly ModelContext modelContext;

        private readonly IServiceScope scope;

        public long UserId { get; set; } = 1;

        public string? Name => modelContext.Principals.Where(x => x.Id == UserId)?.SingleOrDefault()?.Name;

        public UserSession(IServiceScopeFactory factory)
        {
            scope = factory.CreateScope();
            modelContext = scope.ServiceProvider.GetService<ModelContext>()!;
        }
    }
}
