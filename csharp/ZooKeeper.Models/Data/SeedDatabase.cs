namespace Savvy.ZooKeeper.Models.Data;

using System.Globalization;
using System.Threading.Tasks;
using CsvHelper;
using Savvy.ZooKeeper.Models.Entities;
using Savvy.ZooKeeper.Models.Metadata;
using Savvy.ZooKeeper.Models.Security;

public static class SeedDatabase
{
    public record class HabitatRow(string Name, string Description);
    public record class ExhibitRow(string Name, string Type);

    public record class AnimalTypeRow(string Habitat, string Name, string Kingdom, string Phylum, string Class, string Order, string Family, string Genus, string Species, string Diet, string Description, string FeedingTimes);
    public record class AnimalRow(string Name, string AnimalType, AnimalStatus Status, string? Diet, double Age);

    public static async Task Seed(ModelContext modelContext, DirectoryInfo root, CancellationToken ct)
    {
        var system = new Principal { Name = "system" };
        var added = modelContext.Principals.Add(system);
        modelContext.SaveChanges();
        system = added.Entity;

        var p1 = new Principal() { Name = "Bob" };
        var p2 = new Principal() { Name = "Alice" };
        modelContext.Principals.Add(p1);
        modelContext.Principals.Add(p2);
        modelContext.SaveChanges();

        var e1 = new Employee() { Name = "Bob", LastName = "Barker", Email = "price.is.right@gmail.com", Phone = "202-867-5309", CreatedBy = system, UpdatedBy = system, Principal = p1 };
        var e2 = new Employee() { Name = "Alice", LastName = "Liddell", Email = "alice@wonderland.com", Phone = "123-456-7890", CreatedBy = system, UpdatedBy = system, Principal = p2 };
        modelContext.Employees.Add(e1);
        modelContext.Employees.Add(e2);
        modelContext.SaveChanges();

        var role1 = new Role() { Name = "Admin", CreatedBy = system };
        var role2 = new Role() { Name = "Animal Handler", CreatedBy = system };
        modelContext.Roles.Add(role1);
        modelContext.Roles.Add(role2);
        modelContext.SaveChanges();

        var permission1 = new Permission() { Name = "View PII", CreatedBy = system };
        var permission2 = new Permission() { Name = "Update", CreatedBy = system };
        modelContext.Permissions.Add(permission1);
        modelContext.Permissions.Add(permission2);
        modelContext.SaveChanges();

        var rp1 = new RolePermission() { Role = role2, Permission = permission1, CreatedBy = system };
        var rp2 = new RolePermission() { Role = role2, Permission = permission2, CreatedBy = system };
        modelContext.RolePermissions.Add(rp1);
        modelContext.RolePermissions.Add(rp2);
        modelContext.SaveChanges();

        var spr = new PrincipalRole() { Role = role1, Principal = system, CreatedBy = system };
        var pr1 = new PrincipalRole() { Role = role1, Principal = p2, CreatedBy = system };
        var pr2 = new PrincipalRole() { Role = role2, Principal = p1, CreatedBy = system };
        modelContext.PrincipalRoles.Add(spr);
        modelContext.PrincipalRoles.Add(pr1);
        modelContext.PrincipalRoles.Add(pr2);
        modelContext.SaveChanges();

        var fi = new FileInfo(Path.Combine(root.FullName, "Habitat.csv"));

        await LoadTable<Habitat, HabitatRow>(modelContext, fi, (m, r) =>
        {
            m.Name = r.Name;
            m.Description = r.Description;
            m.CreatedById = system.Id;
            m.UpdatedById = system.Id;
        });

        fi = new FileInfo(Path.Combine(root.FullName, "AnimalType.csv"));

        var habitats = modelContext.Habitats.ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);

        await LoadTable<AnimalType, AnimalTypeRow>(modelContext, fi, (m, r) =>
        {
            var h = habitats[r.Habitat];

            m.Name = r.Name;
            m.Habitat = h;
            m.Kingdom = r.Kingdom;
            m.Phylum = r.Phylum;
            m.Class = r.Class;
            m.Order = r.Order;
            m.Family = r.Family;
            m.Genus = r.Genus;
            m.Species = r.Species;
            m.Diet = r.Diet;
            m.Description = r.Description;
            m.FeedingTimes = r.FeedingTimes;
            m.CreatedById = system.Id;
            m.UpdatedById = system.Id;
        });

        fi = new FileInfo(Path.Combine(root.FullName, "exhibits.csv"));

        await LoadTable<Exhibit, ExhibitRow>(modelContext, fi, (m, r) =>
        {
            var h = habitats[r.Type];
            m.Name = r.Name;
            m.Habitat = h;
            m.CreatedById = Random.Shared.NextInt64(2, 4);
            m.UpdatedById = m.CreatedById;
        });

        var exhibits = modelContext.Exhibits.ToDictionary(x => x.Habitat.Name, x => x, StringComparer.InvariantCultureIgnoreCase);
        var animalTypes = modelContext.AnimalTypes.ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);
        fi = new FileInfo(Path.Combine(root.FullName, "Animals.csv"));

        await LoadTable<Animal, AnimalRow>(modelContext, fi, (m, r) =>
        {
            var at = animalTypes[r.AnimalType];
            var e = exhibits[at.Habitat.Name];

            m.Name = r.Name;
            m.AnimalType = at;
            m.Exhibit = e;

            if (!string.IsNullOrWhiteSpace(r.Diet))
            {
                m.Diet = r.Diet;
            }

            m.EnteredCaptivitiy = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero);
            m.Birth = DateTimeOffset.Now.AddDays(r.Age * -365);

            m.CreatedById = Random.Shared.NextInt64(2, 4);
            m.UpdatedById = m.CreatedById;

            var s = new AnimalState { Status = r.Status, CreatedById = Random.Shared.NextInt64(2, 4), Animal = m };
            modelContext.AnimalStates.Add(s);
        });

        foreach (var a in modelContext.AnimalStates)
        {
            a.Animal.CurrentState = a;
        }
        modelContext.SaveChanges();

        foreach (var a in modelContext.Animals.ToList())
        {
            var d1 = DateTimeOffset.Now.AddDays(-1);
            var s1 = new AnimalState { Effective = d1, Status = AnimalStatus.Healthy, CreatedById = Random.Shared.NextInt64(2, 4), Animal = a };
            modelContext.AnimalStates.Add(s1);

            var d2 = DateTimeOffset.Now.AddDays(-2);
            var s2 = new AnimalState { Effective = d2, Status = AnimalStatus.Healthy, CreatedById = Random.Shared.NextInt64(2, 4), Animal = a };
            modelContext.AnimalStates.Add(s2);
        }

        modelContext.SaveChanges();

        modelContext.Notes.Add(new Note { Description = "note attached to nothing", CreatedById = 1, UpdatedById = 1 });
        modelContext.SaveChanges();

        var added1 = modelContext.Notes.Add(new Note { Description = "note attached to employee", CreatedById = 2, UpdatedById = 2 });
        modelContext.SaveChanges();
        modelContext.NoteEntities.Add(new NoteEntity { NoteId = added1.Entity.Id, UserEntityId = modelContext.Employees.First().Id, });
        modelContext.SaveChanges();

        var added2 = modelContext.Notes.Add(new Note { Description = "note attached to employee and exhibit", CreatedById = 3, UpdatedById = 3 });
        modelContext.SaveChanges();
        var ne1 = new NoteEntity { NoteId = added2.Entity.Id, UserEntityId = modelContext.Employees.First().Id, };
        modelContext.NoteEntities.Add(ne1);
        modelContext.SaveChanges();
        var ne2 = new NoteEntity { NoteId = added2.Entity.Id, UserEntityId = modelContext.Exhibits.First().Id, };
        modelContext.NoteEntities.Add(ne2);
        modelContext.SaveChanges();

        var added3 = modelContext.Notes.Add(new Note { Description = "note attached to employee, exhibit, and animal", CreatedById = 2, UpdatedById = 2 });
        modelContext.SaveChanges();
        modelContext.NoteEntities.Add(new NoteEntity { NoteId = added3.Entity.Id, UserEntityId = modelContext.Employees.Skip(1).First().Id });
        modelContext.NoteEntities.Add(new NoteEntity { NoteId = added3.Entity.Id, UserEntityId = modelContext.Exhibits.Skip(1).First().Id, });
        modelContext.NoteEntities.Add(new NoteEntity { NoteId = added3.Entity.Id, UserEntityId = modelContext.Animals.Skip(1).First().Id, });
        modelContext.SaveChanges();

        var added4 = modelContext.Notes.Add(new Note { Description = "note attached to multiple exhibits", CreatedById = 2, UpdatedById = 2 });
        modelContext.SaveChanges();
        modelContext.NoteEntities.Add(new NoteEntity { NoteId = added4.Entity.Id, UserEntityId = modelContext.Exhibits.First().Id });
        modelContext.NoteEntities.Add(new NoteEntity { NoteId = added4.Entity.Id, UserEntityId = modelContext.Exhibits.Skip(1).First().Id, });
        modelContext.NoteEntities.Add(new NoteEntity { NoteId = added4.Entity.Id, UserEntityId = modelContext.Exhibits.Skip(2).First().Id, });
        modelContext.SaveChanges();

        async Task LoadTable<TModel, TRow>(ModelContext context, FileInfo f, Action<TModel, TRow> callback)
            where TModel : class, new()
        {
            using var reader = new StreamReader(fi.FullName);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecordsAsync<TRow>(ct)
                .ConfigureAwait(false);

            await foreach (var r in records)
            {
                var m = new TModel();
                callback(m, r);
                modelContext.Set<TModel>().Add(m);
            }

            await modelContext.SaveChangesAsync(ct)
                .ConfigureAwait(false);
        }
    }
}