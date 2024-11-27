namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;

    public record class CreateAnimal(string Name, long AnimalTypeId, string? Diet, string? FeedingTimes);

    [ApiController]
    [Route("graphql")]
    public class GraphQLController : ControllerBase
    {
        protected readonly ModelContext database;

        public GraphQLController(ModelContext modelContext)
        {
            database = modelContext;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Animal> CreateAnimal(CreateAnimal create)
        {
            var animal = new Animal();
            animal.Id = 0;
            animal.Name = create.Name;
            animal.AnimalTypeId = create.AnimalTypeId;
            animal.CreatedById = 2;
            animal.UpdatedById = 2;
            animal.Diet = create.Diet;
            animal.FeedingTimes = create.FeedingTimes;
            var result = database.Animals.Add(animal);
            database.SaveChanges();
            var created = database.Animals.Include(x => x.AnimalType).Single(x => x.Id == result.Entity.Id);
            return Created("get", created);
        }
    }
}
