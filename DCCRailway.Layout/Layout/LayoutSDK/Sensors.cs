using DCCRailway.LayoutService.Layout.Entities;

namespace DCCRailway.LayoutService.Layout.LayoutSDK;

public class Sensors(string serviceUrl) : Entity<Sensor>(serviceUrl, "sensors");