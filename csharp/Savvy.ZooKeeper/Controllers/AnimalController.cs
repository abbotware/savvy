namespace Savvy.ZooKeeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Models.Entities;

    [ApiController]
    [Route("animal")]
    public class AnimalController : BaseCrudController<Animal>
    {
        public AnimalController(ModelContext modelContext) 
            : base(modelContext)
        {
        }

        protected override IQueryable<Animal> OnQuery(ModelContext modelContext)
        {
            return modelContext.Animals
                .Include(x => x.CurrentState)
                .Include(x => x.Notes)
                .Include(x => x.CurrentExhibit)
                .Include(x => x.AnimalType)
                .AsQueryable();
        }
    }
}
