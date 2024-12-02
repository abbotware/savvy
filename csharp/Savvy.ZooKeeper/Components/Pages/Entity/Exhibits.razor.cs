namespace Savvy.ZooKeeper.Components.Pages.Entity
{
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;

    public partial class Exhibits
    {
        IReadOnlyList<Note> Notes { get; set; } = Array.Empty<Note>();

        protected override IQueryable<Exhibit> OnQuery(ModelContext modelContext)
        {
            return modelContext.Exhibits
                .Include(x => x.Animals)
                .Include(x => x.Habitat)
                .AsQueryable();
        }

        protected override void OnSelectedRow(Exhibit selectedRow)
        {
            Notes = Database.NoteEntities
                .Include(x => x.Note)
                .ThenInclude(x => x.CreatedBy)
                .Where(x => x.UserEntityId == selectedRow.Id)
                .Select(x => x.Note)
                .ToList();
        }
    }
}