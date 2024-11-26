namespace Savvy.ZooKeeper.Models.Data;

using System.Globalization;
using System.Threading.Tasks;
using CsvHelper;
using Savvy.ZooKeeper.Models.Metadata;

public static class SeedDatabase
{
    public record class HabitatRow(string Name, string Description);
    public record class AnimalTypeRow(string Habitat, string Name, string Kingdom, string Phylum, string Class, string Order, string Family, string Genus, string Species, string Diet);

    public static async Task Seed(ModelContext modelContext, DirectoryInfo root, CancellationToken ct)
    {
        var fi = new FileInfo(Path.Combine(root.FullName, "Habitat.csv"));

        await LoadTable<Habitat, HabitatRow>(modelContext, fi, (m, r) =>
        {
            m.Name = r.Name;
            m.Description = r.Description;
        });

        fi = new FileInfo(Path.Combine(root.FullName, "AnimalType.csv"));

        var habs = modelContext.Habitats.ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);

        await LoadTable<AnimalType, AnimalTypeRow>(modelContext, fi, (m, r) =>
        {
            var h = habs[r.Habitat];

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
        });


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