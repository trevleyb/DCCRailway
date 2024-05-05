using DCCRailway.LayoutService.Layout.Entities;

namespace DCCRailway.LayoutService.Layout.LayoutSDK;

public class Blocks(string serviceUrl) : Entity<Block>(serviceUrl, "blocks");