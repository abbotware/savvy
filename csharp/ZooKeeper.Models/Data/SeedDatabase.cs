namespace Savvy.ZooKeeper.Models.Data;

using System.Globalization;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using CsvHelper;
using Savvy.ZooKeeper.Models.Entities;
using Savvy.ZooKeeper.Models.Metadata;

public static class SeedDatabase
{
    public record class HabitatRow(string Name, string Description);
    public record class ExhibitRow(string Name, string Type);

    public record class AnimalTypeRow(string Habitat, string Name, string Kingdom, string Phylum, string Class, string Order, string Family, string Genus, string Species, string Diet, string Description, string FeedingTimes);
    public record class AnimalRow(string Name, string AnimalType, AnimalStatus Status, string? Diet, double Age);

    public static async Task Seed(ModelContext modelContext, DirectoryInfo root, CancellationToken ct)
    {

        var system = new Principal { Name = "system"};
        var saved2  = modelContext.Principals.Add(system);
        modelContext.SaveChanges();
        system = saved2.Entity;

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
            m.CreatedById = system.Id;
            m.UpdatedById = system.Id;
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
            m.CurrentExhibit = e;
            
            if (!string.IsNullOrWhiteSpace(r.Diet))
            {
                m.Diet = r.Diet;
            }

            m.EnteredCaptivitiy = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero);
            m.Birth = DateTimeOffset.Now.AddDays(r.Age * -365);

            m.CreatedById = system.Id;
            m.UpdatedById = system.Id;
        });

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