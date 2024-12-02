using System.Xml;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Entities;
using Syncfusion.Blazor.Grids;

namespace Savvy.ZooKeeper.Components.Pages
{
    public partial class Animals
    {
        IReadOnlyList<Note> Notes { get; set; } = Array.Empty<Note>();

        IReadOnlyList<AnimalState> States { get; set; } = Array.Empty<AnimalState>();

        protected override IQueryable<Animal> OnQuery(ModelContext modelContext)
        {
            return modelContext.Animals
                .Include(x=> x.CreatedBy)
                .Include(x => x.CurrentState!)
                .ThenInclude(x => x.CreatedBy)
                .Include(x => x.Notes)
                .ThenInclude(x=> x.CreatedBy)
                .Include(x => x.Exhibit)
                .Include(x => x.AnimalType)
                .AsQueryable();
        }
        protected override void OnSelectedRow(Animal selectedRow)
        {
            Notes = Database.NoteEntities
                .Include(x => x.Note)
                .ThenInclude(x => x.CreatedBy)
                .Where(x => x.UserEntityId == selectedRow.Id)
                .Select(x => x.Note)
                .ToList();

            States = Database.AnimalStates
                .Include(x => x.CreatedBy)
                .Where(x => x.AnimalId == selectedRow.Id)
                .OrderByDescending(x => x.Effective)
                .ToList();
        }
    }
}