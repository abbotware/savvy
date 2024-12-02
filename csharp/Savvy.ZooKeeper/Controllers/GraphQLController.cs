namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;
    using Savvy.ZooKeeper.Services;

    public record class CreateAnimal(string Name, long AnimalTypeId, string? Diet, string? FeedingTimes);

    [ApiController]
    [Route("graphql")]
    public class GraphQLController : ControllerBase
    {
        protected readonly ModelContext Database;

        protected readonly IUserSession UserSession;

        public GraphQLController(ModelContext modelContext, IUserSession userSession)
        {
            Database = modelContext;
            UserSession = userSession;
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
            animal.CreatedById = UserSession.UserId;
            animal.UpdatedById = UserSession.UserId;
            animal.Diet = create.Diet;
            animal.FeedingTimes = create.FeedingTimes;
            var result = Database.Animals.Add(animal);
            Database.SaveChanges();
            var created = Database.Animals.Include(x => x.AnimalType).Single(x => x.Id == result.Entity.Id);
            return Created("get", created);
        }
    }
}
