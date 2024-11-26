using Microsoft.AspNetCore.Components;
using Savvy.ZooKeeper.Models;

namespace Savvy.ZooKeeper.Components.Pages
{
    public partial class Admin
    {
        [Inject]
        private ModelContext ModelContext { get; set; } = null!;

        async Task OnReseedDatabase()
        {
            await ModelContext.Database.EnsureDeletedAsync();
            await ModelContext.Database.EnsureCreatedAsync();
        }
    }
}