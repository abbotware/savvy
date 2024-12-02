namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;
    using Savvy.ZooKeeper.Services;

    [ApiController]
    [Route("animal")]
    public class AnimalController : BaseDBController<Animal>
    {
        public record CreateExhibit(string Name, long HabitatId);

        public record class CreateAnimal(string Name, long AnimalTypeId, string? Diet, string? FeedingTimes)
        {
            public long? ExhibitId { get; set; }

            public CreateExhibit? Exhibit { get; set; }
        }

        public AnimalController(ModelContext modelContext, IUserSession userSession) 
            : base(modelContext, userSession)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<Animal>> Get([FromQuery(Name ="in_exhibit")]long? exhibitId, [FromQuery(Name = "needing_attention")] bool? needingAttention)
        {
            var query = OnQuery(Database);

            if (exhibitId.HasValue)
            {
                query = query.Where(x => x.ExhibitId == exhibitId);  
            }

            var intermediate = query.ToList();

            if (needingAttention.HasValue)
            {
                intermediate = intermediate.Where(x => x.CurrentStatus != AnimalStatus.Healthy).ToList();
            }

            return Ok(intermediate);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Animal> Post(CreateAnimal create)
        {
            Exhibit? exhibit = null;

            if (string.IsNullOrWhiteSpace(create.Name)) {
                return BadRequest("Animal Name can not be blank");
            }

            if (Database.Animals.SingleOrDefault(x => x.Name == create.Name) is not null)
            {
                return BadRequest($"Animal Name:{create.Name} already in use");
            }

            if (create.ExhibitId.HasValue && create.Exhibit is not null)
            {
                return BadRequest("ExhibitId and Exhibit can not both be set");
            }

            if (create.ExhibitId.HasValue)
            {
                exhibit = Database.Exhibits.SingleOrDefault(x => x.Id == create.ExhibitId);

                if (exhibit is null)
                {
                    return BadRequest($"ExhibitId:{create.ExhibitId} not found");
                }
            }

            if (create.Exhibit is not null)
            {
                if (string.IsNullOrWhiteSpace(create.Exhibit.Name))
                {
                    return BadRequest("Exhibit Name can not be blank");
                }

                if (Database.Habitats.SingleOrDefault(x => x.Id == create.Exhibit.HabitatId) is null)
                {
                    return BadRequest($"HabitatId:{create.Exhibit.HabitatId} not found");
                }

                exhibit = new Exhibit();
                exhibit.Name = create.Exhibit.Name;
                exhibit.CreatedById = UserSession.UserId;
                exhibit.UpdatedById = exhibit.CreatedById;
                exhibit.HabitatId = create.Exhibit.HabitatId;
                Database.Exhibits.Add(exhibit);
            }

            if (Database.AnimalTypes.SingleOrDefault(x => x.Id == create.AnimalTypeId) is null)
            {
                return BadRequest($"AnimalTypeId:{create.AnimalTypeId} not found");
            }

            var animal = new Animal();
            animal.Id = 0;
            animal.Name = create.Name;
            animal.AnimalTypeId = create.AnimalTypeId;
            animal.CreatedById = UserSession.UserId;
            animal.UpdatedById = animal.CreatedById;
            animal.Exhibit = exhibit;
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
               .Include(x => x.CreatedBy)
               .Include(x => x.CurrentState!)
               .ThenInclude(x => x.CreatedBy)
               .Include(x => x.NoteEntities)
               .ThenInclude(x => x.Note)
               .ThenInclude(x => x.CreatedBy)
               .Include(x => x.Exhibit)
               .Include(x => x.AnimalType)
               .ThenInclude(x => x.Habitat);
        }
    }
}
