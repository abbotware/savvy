namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Metadata;

    [ApiController]
    [Route("metadata/habitat")]
    public class HabitatController : BaseCrudController<Habitat>
    {
        public HabitatController(ModelContext modelContext) 
            : base(modelContext)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Habitat> Post(string name, string? desciription)
        {
            var result = database.Habitats.Add(new Habitat { Name = name, Description = desciription });
            database.SaveChanges();
            return Created("get", result.Entity);
        }


        [HttpGet("{id}/animaltypes")]
        [ProducesResponseType(typeof(List<AnimalType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AnimalType>> AnimalTypes(int id)
        {
            var animals = database.Habitats.SingleOrDefault(x => x.Id == id)?.AnimalTypes ?? Array.Empty<AnimalType>();

            return Ok(animals);
        }
    }
}
