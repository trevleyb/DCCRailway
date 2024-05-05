using DCCRailway.LayoutService.Layout.Entities;

namespace DCCRailway.LayoutService.Layout.LayoutSDK;

public class Turnouts(string serviceUrl) : Entity<Turnout>(serviceUrl, "turnouts");