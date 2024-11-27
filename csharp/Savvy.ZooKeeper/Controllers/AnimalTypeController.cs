namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;
    using Savvy.ZooKeeper.Models.Metadata;

    [ApiController]
    [Route("metadata/animaltype")]
    public class AnimalTypeController : BaseCrudController<AnimalType>
    {
        public AnimalTypeController(ModelContext modelContext)
            : base(modelContext)
        {
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<AnimalType> Post(string name, string? desciription)
        {
            var result = database.AnimalTypes.Add(new AnimalType { Name = name, Description = desciription });
            database.SaveChanges();
            return Created("get", result.Entity);
        }


        [HttpGet("{id}/animals")]
        [ProducesResponseType(typeof(List<Animal>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Animal>> Animals(int id)
        {
            var animals = database.Animals.Where(x => x.AnimalTypeId == id);

            return Ok(animals);
        }
    }
}