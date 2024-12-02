﻿namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;
    using Savvy.ZooKeeper.Services;


    [ApiController]
    [Route("note")]
    public class NoteController : BaseCrudController<Note>
    {
        public record class CreateNote(string Text);

        public NoteController(ModelContext modelContext, IUserSession userSession)
            : base(modelContext, userSession)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Note> Post([FromBody] CreateNote note)
        {
            var result = Database.Notes.Add(new Note
            {
                Name = string.Empty,
                Description = note.Text,
                CreatedById = UserSession.UserId,
                UpdatedById = UserSession.UserId,
            });
            Database.SaveChanges();
            return Created("get", result.Entity);
        }

        [HttpPut("{id}/link/{entityId}")]
        [ProducesResponseType(typeof(List<Animal>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Animal>> Link(int id, long entityId)
        {
            var mapped = Database.NoteEntities.SingleOrDefault(x => x.NoteId == id && x.UserEntityId == entityId);

            if (mapped is not null)
            {
                return Ok();
            }

            var note = Database.Notes.SingleOrDefault(x => x.Id == id);
            var entity = Database.Entities.SingleOrDefault(x => x.Id == entityId);

            if (note is null || entity is null)
            {
                return NotFound();
            }

            var create = new NoteEntity();
            create.NoteId = id;
            create.UserEntityId = entityId;
            Database.NoteEntities.Add(create);
            Database.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}/unlink/{entityId}")]
        [ProducesResponseType(typeof(List<Animal>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Animal>> Unlink(int id, long entityId)
        {
            var note = Database.Notes.SingleOrDefault(x => x.Id == id);
            var entity = Database.Entities.SingleOrDefault(x => x.Id == entityId);

            if (note is null || entity is null)
            {
                return NotFound();
            }

            var mapped = Database.NoteEntities.SingleOrDefault(x => x.NoteId == id && x.UserEntityId == entityId);

            if (mapped is null)
            {
                return Ok();
            }

            Database.NoteEntities.Remove(mapped);
            Database.SaveChanges();

            return Ok();
        }

        protected override IQueryable<Note> OnQuery(ModelContext modelContext)
        {
            return modelContext.Notes
                .Include(x => x.CreatedBy)
                .Include(x => x.NoteOf)
                .AsQueryable();
        }
    }
}