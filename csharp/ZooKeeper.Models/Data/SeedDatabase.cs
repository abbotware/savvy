namespace Savvy.ZooKeeper.Models.Data;

using System.Globalization;
using System.Threading.Tasks;
using CsvHelper;

public static class SeedDatabase
{
    public record class HabitatRow(string Name, string Description);

    public static async Task Seed(ModelContext modelContext, DirectoryInfo root, CancellationToken ct)
    {
        var fi = new FileInfo(Path.Combine(root.FullName, "Habitat.csv"));

        await LoadTable<Habitat, HabitatRow>(modelContext, fi, (m, r) =>
        {
            m.Name = r.Name;
            m.Description = r.Description;
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