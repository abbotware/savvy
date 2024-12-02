namespace Savvy.ZooKeeper.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NSubstitute;
    using Savvy.ZooKeeper.Controllers;
    using Savvy.ZooKeeper.Models;
    using Savvy.ZooKeeper.Services;

    public class Example
    {
        [Test]
        public void SimpleTest()
        {
            // MOCK
            var session = Substitute.For<IUserSession>();

            // use in memory database
            var options = new DbContextOptionsBuilder<ModelContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var ac = new AnimalController(new ModelContext(options), session);

            var rows = ac.Get(null, null);

            Assert.That(rows, Is.Not.Null);
        }
    }
}
