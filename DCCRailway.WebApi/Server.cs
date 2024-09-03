using DCCRailway.Layout;
using DCCRailway.Layout.Entities;
using DCCRailway.WebApi.Layout;
using ILogger = Serilog.ILogger;
using Route = DCCRailway.Layout.Entities.Route;

namespace DCCRailway.WebApi;

public class Server(ILogger logger, IRailwaySettings railwaySettings) {
    private CancellationTokenSource cts = new();

    public async Task Start() {
        logger.Information("Starting DCCRailway WebAPI Server");
        var options = new WebApplicationOptions {
            Args = new[] { $"--urls=http://localhost:{railwaySettings.WebApiPort}" }
        };

        var builder = WebApplication.CreateBuilder(options);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton(railwaySettings.Accessories);
        builder.Services.AddSingleton(railwaySettings.Blocks);
        builder.Services.AddSingleton(railwaySettings.Locomotives);
        builder.Services.AddSingleton(railwaySettings.Routes);
        builder.Services.AddSingleton(railwaySettings.Sensors);
        builder.Services.AddSingleton(railwaySettings.Signals);
        builder.Services.AddSingleton(railwaySettings.Turnouts);

        var app = builder.Build();
        ApiHelper.MapEntity<Accessories, Accessory>(app, "accessories");
        ApiHelper.MapEntity<Blocks, Block>(app, "blocks");
        ApiHelper.MapEntity<Locomotives, Locomotive>(app, "locomotives");
        ApiHelper.MapEntity<Routes, Route>(app, "routes");
        ApiHelper.MapEntity<Sensors, Sensor>(app, "sensors");
        ApiHelper.MapEntity<Signals, Signal>(app, "signals");
        ApiHelper.MapEntity<Turnouts, Turnout>(app, "turnouts");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        logger.Information($"DCCRailway WebAPI Server running on ");
        await app.RunAsync(cts.Token);
    }

    public void Stop() {
        cts.Cancel();
    }
}