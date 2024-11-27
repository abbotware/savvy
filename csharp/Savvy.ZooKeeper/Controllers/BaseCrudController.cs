namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savvy.ZooKeeper.Models;

    public class BaseCrudController<T> : ControllerBase
        where T : class, IInsertable
    {
        protected readonly ModelContext database;

        protected virtual IQueryable<T> OnQuery(ModelContext context) => context.Set<T>().AsQueryable();


        public BaseCrudController(ModelContext modelContext)
        {
            database = modelContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<T>> Get()
        {
            return Ok(OnQuery(database));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<T> Get(int id)
        {
            return Ok(OnQuery(database).SingleOrDefault( x=> x.Id == id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(int id)
        {
            var found = database.Set<T>().SingleOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound();
            }

            database.Remove(found);
            database.SaveChanges();

            return Ok();
        }
    }
}
