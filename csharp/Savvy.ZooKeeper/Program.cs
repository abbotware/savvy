using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Components;
using Savvy.ZooKeeper.Components.Pages;
using Savvy.ZooKeeper.Models;
using Syncfusion.Blazor;

using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Savvy.ZooKeeper.Services;
using CsvHelper;
using Savvy.ZooKeeper.Models.Data;
using System.Text.Json.Serialization;

namespace Savvy.ZooKeeper;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddControllers();
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddDbContext<ModelContext>(o => o.UseSqlServer("name=Database"));
        builder.Services.AddOpenApi();
        builder.Services.AddSyncfusionBlazor();

        builder.Services.AddSingleton<IUserSession, UserSession>();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU5NTc0MUAzMjM3MmUzMDJlMzBPc1VJV2ZxNDJPNndTRDkvc0ZGQUhORUVHZk1wU0x4NXlleWV0QlNsejlBPQ==");

        var app = builder.Build();

        SeedDatabase(app);

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.MapOpenApi();
        app.MapScalarApiReference();


        app.UseHttpsRedirection();
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapControllers();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }

    private static void SeedDatabase(WebApplication app)
    {
        IServiceScope s;
        ModelContext db;
        s = app.Services.CreateScope();
        db = s.ServiceProvider.GetService<ModelContext>()!;
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        var di = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "data"));
        Models.Data.SeedDatabase.Seed(db, di, default).GetAwaiter().GetResult();
    }
}
