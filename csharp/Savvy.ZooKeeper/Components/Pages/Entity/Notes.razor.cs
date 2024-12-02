namespace Savvy.ZooKeeper.Components.Pages.Entity
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;
    using Savvy.ZooKeeper.Services;

    public partial class Notes
    {
        [Inject]
        IUserSession UserSession { get; set; } = null!;

        IReadOnlyList<Entity> Entities { get; set; } = Array.Empty<Entity>();

        protected override IQueryable<Note> OnQuery(ModelContext modelContext)
        {
            var query = modelContext.Notes.AsQueryable();

            if (!UserSession.IsAdmin)
            {
                query = query.Where(x => x.CreatedById == UserSession.UserId);
            }

            return query
                    .Include(x => x.CreatedBy)
                    .Include(x => x.NoteOf)
                    .ThenInclude(x => x.Entity);
        }

        protected override void OnSelectedRow(Note selectedRow)
        {
            Entities = selectedRow.NoteOf.Select(x => x.Entity).ToList();
        }
    }
}