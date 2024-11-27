using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Entities;
using Syncfusion.Blazor.Grids;

namespace Savvy.ZooKeeper.Components.Pages
{
    public partial class Exhibits
    {
        protected override IQueryable<Exhibit> OnQuery(ModelContext modelContext)
        {
            return modelContext.Exhibits
                .Include(x => x.Animals)
                .Include(x => x.Habitat)
                .AsQueryable();
        }
    }
}