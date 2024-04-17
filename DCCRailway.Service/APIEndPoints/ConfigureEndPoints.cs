namespace DCCRailway.Service.APIEndPoints;

public static class ConfigureAPIEndPoints {
    public static void ConfigureAPIs(this WebApplication app) {
        LocomotiveAPI.Configure(app);
    }
}