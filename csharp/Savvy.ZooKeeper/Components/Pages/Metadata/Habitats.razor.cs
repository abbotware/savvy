using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Metadata;

namespace Savvy.ZooKeeper.Components.Pages.Metadata
{
    public partial class Habitats
    {
        protected override IQueryable<Habitat> OnQuery(ModelContext modelContext)
        {
            return modelContext.Habitats
                    .Include(x => x.CreatedBy)
                    .Include(x => x.AnimalTypes);
        }
    }
}