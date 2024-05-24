using DCCRailway.Layout;
using DCCRailway.Layout.Entities;
using DCCRailway.WebApi.Layout;
using ILogger = Serilog.ILogger;

namespace DCCRailway.WebApi;

public class Server(ILogger logger, IRailwaySettings railwaySettings) {
    private CancellationTokenSource cts = new();

    public async void Start() {
        var builder = WebApplication.CreateBuilder();

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
        APIHelper.MapEntity<Accessories, Accessory>(app, "accessories");
        APIHelper.MapEntity<Blocks, Block>(app, "blocks");
        APIHelper.MapEntity<Locomotives, Locomotive>(app, "locomotives");
        APIHelper.MapEntity<Routes, DCCRailway.Layout.Entities.Route>(app, "routes");
        APIHelper.MapEntity<Sensors, Sensor>(app, "sensors");
        APIHelper.MapEntity<Signals, Signal>(app, "signals");
        APIHelper.MapEntity<Turnouts, Turnout>(app, "turnouts");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        await app.RunAsync(cts.Token);
    }

    public void Stop() {
        cts.Cancel();
    }
}