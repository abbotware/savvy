namespace Savvy.ZooKeeper.Components.Pages.Entity
{
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;

    public partial class Inhabitants
    {
        IReadOnlyList<Note> Notes { get; set; } = Array.Empty<Note>();

        IReadOnlyList<AnimalState> States { get; set; } = Array.Empty<AnimalState>();

        protected override IQueryable<Animal> OnQuery(ModelContext modelContext)
        {
            return modelContext.Animals
                .Include(x => x.CreatedBy)
                .Include(x => x.CurrentState!)
                .ThenInclude(x => x.CreatedBy)
                .Include(x => x.Notes)
                .ThenInclude(x => x.CreatedBy)
                .Include(x => x.Exhibit)
                .Include(x => x.AnimalType)
                .ThenInclude(x => x.Habitat);
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