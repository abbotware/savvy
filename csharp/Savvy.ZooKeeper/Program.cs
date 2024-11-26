using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Components;
using Savvy.ZooKeeper.Components.Pages;
using Savvy.ZooKeeper.Models;
using Syncfusion.Blazor;

using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

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

        builder.Services.AddDbContext<ModelContext>(o =>o.UseSqlServer("name=Database"));
        builder.Services.AddOpenApi();
        builder.Services.AddSyncfusionBlazor();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU5NTc0MUAzMjM3MmUzMDJlMzBPc1VJV2ZxNDJPNndTRDkvc0ZGQUhORUVHZk1wU0x4NXlleWV0QlNsejlBPQ==");

        var app = builder.Build();

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
}
