using DCCRailway.Layout;
using DCCRailway.WebApp.Components;
using ILogger = Serilog.ILogger;

namespace DCCRailway.WebApp;

public class Server(ILogger logger, IRailwaySettings railwaySettings) {
    public async Task Start() {
        var options = new WebApplicationOptions {
            Args = new[] { $"--urls=http://localhost:{railwaySettings.WebAppPort}" }
        };

        var builder = WebApplication.CreateBuilder(options);

        // Add services to the container.
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error", true);

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

        logger.Information($"DCCRailway WebAPI Server running on ");
        app.Run();
    }
}