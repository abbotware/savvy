using Microsoft.AspNetCore.Components;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Data;

namespace Savvy.ZooKeeper.Components.Pages
{
    public partial class Admin
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        [Inject]
        private ModelContext ModelContext { get; set; } = null!;

        [Inject]
        private IWebHostEnvironment webHostEnvironment { get; set; } = null!;

        public IReadOnlyList<Principal> Principals => ModelContext.Principals.ToList();

        public IReadOnlyList<Role> Roles => ModelContext.Roles.ToList();

        public IReadOnlyList<Permission> Permissions => ModelContext.Permissions.ToList();


        private async Task OnReseedDatabase()
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                ModelContext.Database.EnsureDeleted();
                ModelContext.Database.EnsureCreated();

                var di = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "data"));

                await SeedDatabase.Seed(ModelContext, di, default);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}