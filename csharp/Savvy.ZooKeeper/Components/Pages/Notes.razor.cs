using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Entities;
using Savvy.ZooKeeper.Services;

namespace Savvy.ZooKeeper.Components.Pages
{
    public partial class Notes
    {
        [Inject]
        IUserSession UserSession { get; set; } = null!;

        protected override IQueryable<Note> OnQuery(ModelContext modelContext)
        {
            return modelContext.Notes.Where(x => x.CreatedById == UserSession.UserId)
                .Include(x => x.CreatedBy)
                .Include(x => x.NoteOf)
                .AsQueryable();
        }
    }
}