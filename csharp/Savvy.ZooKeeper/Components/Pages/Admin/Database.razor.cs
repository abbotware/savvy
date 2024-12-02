using Microsoft.AspNetCore.Components;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Data;

namespace Savvy.ZooKeeper.Components.Pages.Admin
{
    public partial class Database
    {
        private static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        [Inject]
        private ModelContext ModelContext { get; set; } = null!;

        [Inject]
        private IWebHostEnvironment webHostEnvironment { get; set; } = null!;

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