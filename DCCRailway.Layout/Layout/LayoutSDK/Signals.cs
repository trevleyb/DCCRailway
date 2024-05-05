using DCCRailway.LayoutService.Layout.Entities;

namespace DCCRailway.LayoutService.Layout.LayoutSDK;

public class Signals(string serviceUrl) : Entity<Signal>(serviceUrl, "signals");