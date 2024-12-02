namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Metadata;
    using Savvy.ZooKeeper.Services;

    [ApiController]
    [Route("metadata/habitat")]
    public class HabitatController : BaseCrudController<Habitat>
    {
        public HabitatController(ModelContext modelContext, IUserSession userSession) 
            : base(modelContext, userSession)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Habitat> Post(string name, string? desciription)
        {
            var result = Database.Habitats.Add(new Habitat { 
                Name = name, 
                Description = desciription,
                CreatedById = UserSession.UserId,
                UpdatedById = UserSession.UserId,
            });
            Database.SaveChanges();
            return Created("get", result.Entity);
        }


        [HttpGet("{id}/animaltypes")]
        [ProducesResponseType(typeof(List<AnimalType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AnimalType>> AnimalTypes(int id)
        {
            var animals = Database.Habitats.SingleOrDefault(x => x.Id == id)?.AnimalTypes ?? Array.Empty<AnimalType>();

            return Ok(animals);
        }
    }
}
