using Microsoft.AspNetCore.Mvc;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Metadata;

namespace Savvy.ZooKeeper.Controllers
{
    [ApiController]
    [Route("metadata/habitat")]
    public class HabitatController : ControllerBase
    {
        private readonly ModelContext database;

        public HabitatController(ModelContext modelContext)
        {
            database = modelContext;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<Habitat>> Get()
        {
            return Ok(database.Habitats);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Habitat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Habitat> Get(int id)
        {
            return Ok(database.Habitats.SingleOrDefault(x => x.Id == id));
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Habitat> Post(string name, string? desciription)
        {
            var result = database.Habitats.Add(new Habitat { Name = name, Description = desciription });
            database.SaveChanges();
            return Created("get", result.Entity);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Habitat value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("{id}/animaltypes")]
        [ProducesResponseType(typeof(List<AnimalType>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AnimalType>> AnimalTypes(int id) 
        {
            var animals = database.Habitats.SingleOrDefault(x => x.Id == id)?.AnimalTypes ?? Array.Empty<AnimalType>();

            return Ok(animals);
        }
    }
}
