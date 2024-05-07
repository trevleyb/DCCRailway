using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.EndPoints;

public static class StateAPI {

    public static void Configure(WebApplication app, StateManager stateManager) {

        app.MapGet("/states", async() => await Task.FromResult(stateManager.GetAll()));
        app.MapGet("/states/{id}", async (string id) => await Task.FromResult(stateManager.GetState(id)));
        app.MapDelete("/states/{id}", async (string id) => stateManager.DeleteState(id));
        app.MapPut("/states/{id}", async (string id, StateObject state) => await Task.FromResult(stateManager.SetState(state)));
        app.MapPost("/states/{id}", async (string id, StateObject state) => await Task.FromResult(stateManager.SetState(state)));

    }
}