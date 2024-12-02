namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Services;

    public class BaseDBController<T> : ControllerBase
        where T : class, IIdentifiable<long>
    {
        protected readonly ModelContext Database;

        protected readonly IUserSession UserSession;

        public BaseDBController(ModelContext modelContext, IUserSession userSession)
        {
            Database = modelContext;
            UserSession = userSession;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<T> Get(int id)
        {
            var found = OnQuery(Database).SingleOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(int id)
        {
            var found = Database.Set<T>().SingleOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound();
            }

            Database.Remove(found);
            Database.SaveChanges();

            return Ok();
        }

        protected virtual IQueryable<T> OnQuery(ModelContext context) => context.Set<T>().AsQueryable();
    }
}
