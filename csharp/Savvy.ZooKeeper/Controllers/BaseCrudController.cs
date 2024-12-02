namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Services;

    public class BaseCrudController<T> : BaseDBController<T>
        where T : class, IIdentifiable<long>
    {
        public BaseCrudController(ModelContext modelContext, IUserSession userSession)
            : base(modelContext, userSession)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<T>> Get()
        {
            return Ok(OnQuery(Database));
        }
    }
}
