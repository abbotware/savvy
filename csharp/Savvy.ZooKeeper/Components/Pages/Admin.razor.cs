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

        async Task OnReseedDatabase()
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
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                semaphoreSlim.Release();
            }
        }
    }
}