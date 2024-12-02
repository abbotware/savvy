namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;
    using Savvy.ZooKeeper.Models.Metadata;
    using Savvy.ZooKeeper.Services;

    [ApiController]
    [Route("metadata/animaltype")]
    public class AnimalTypeController : BaseCrudController<AnimalType>
    {
        public AnimalTypeController(ModelContext modelContext, IUserSession userSession)
            : base(modelContext, userSession)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<AnimalType> Post(string name, string? desciription)
        {
            var result = Database.AnimalTypes.Add(new AnimalType { 
                Name = name, 
                Description = desciription,
                CreatedById = UserSession.UserId,
                UpdatedById = UserSession.UserId,
            });
            Database.SaveChanges();
            return Created("get", result.Entity);
        }

        [HttpGet("{id}/animals")]
        [ProducesResponseType(typeof(List<Animal>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Animal>> Animals(int id)
        {
            var animals = Database.Animals.Where(x => x.AnimalTypeId == id);

            return Ok(animals);
        }
    }
}