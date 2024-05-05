using DCCRailway.LayoutService.Layout.Entities;

namespace DCCRailway.LayoutService.Layout.LayoutSDK;

public class Locomotives(string serviceUrl) : Entity<Locomotive>(serviceUrl, "locomotives");