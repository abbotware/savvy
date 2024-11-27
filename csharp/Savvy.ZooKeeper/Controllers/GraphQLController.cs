namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;

    public record class CreateAnimal(string Name, long AnimalTypeId);

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
            var result = database.Animals.Add(animal);
            database.SaveChanges();
            return Created("get", result.Entity);
        }
    }
}
