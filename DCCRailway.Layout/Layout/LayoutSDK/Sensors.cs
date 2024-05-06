using DCCRailway.Layout.Layout.Entities;

namespace DCCRailway.Layout.Layout.LayoutSDK;

public class Sensors(string serviceUrl) : Entity<Sensor>(serviceUrl, "sensors");