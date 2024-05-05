using DCCRailway.LayoutService.Layout.Entities;
using Route = DCCRailway.LayoutService.Layout.Entities.Route;

namespace DCCRailway.LayoutService.Layout.LayoutSDK;

public class Routes(string serviceUrl) : Entity<Route>(serviceUrl, "routes");