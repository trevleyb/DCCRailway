using Entities_Route = DCCRailway.Layout.Layout.Entities.Route;

namespace DCCRailway.Layout.Layout.LayoutSDK;

public class Routes(string serviceUrl) : Entity<Entities_Route>(serviceUrl, "routes");