using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Entities;
using Syncfusion.Blazor.Grids;

namespace Savvy.ZooKeeper.Components.Pages
{
    public partial class Animals
    {

        protected override IQueryable<Animal> OnQuery(ModelContext modelContext)
        {
            return modelContext.Animals
                .Include(x => x.CurrentState)
                .Include(x => x.Notes)
                .Include(x => x.CurrentExhibit)
                .Include(x => x.AnimalType)
                .AsQueryable();
        }
    }
}