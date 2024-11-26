using Microsoft.AspNetCore.Components;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Data;

namespace Savvy.ZooKeeper.Components.Pages
{
    public partial class Admin
    {
        [Inject]
        private ModelContext ModelContext { get; set; } = null!;

        [Inject]
        private IWebHostEnvironment webHostEnvironment { get; set; } = null!;

        async Task OnReseedDatabase()
        {
            await ModelContext.Database.EnsureDeletedAsync();
            await ModelContext.Database.EnsureCreatedAsync();

            var di = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "data"));

            await SeedDatabase.Seed(ModelContext, di, default);
        }
    }
}