using DCCRailway.Common.Utilities;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.Layout;

namespace DCCRailway.DCCController.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutGenericCmdUpdater(DCCRailwayConfig config)  {
    protected DCCRailwayConfig Config { get; } = config;

    public bool Process(ICommand command) {
        Logger.Log.Information($"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");
        return true;
    }
}