namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;
    using Savvy.ZooKeeper.Services;

    [ApiController]
    [Route("animal")]
    public class AnimalController : BaseCrudController<Animal>
    {
        public record class Create(string Name, long AnimalTypeId, string? Diet, string? FeedingTimes);


        public AnimalController(ModelContext modelContext, IUserSession userSession) 
            : base(modelContext, userSession)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Animal> CreateAnimal(Create create)
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

        protected override IQueryable<Animal> OnQuery(ModelContext modelContext)
        {
            return modelContext.Animals
                .Include(x => x.CurrentState)
                .Include(x => x.Notes)
                .Include(x => x.Exhibit)
                .Include(x => x.AnimalType);
        }
    }
}
