using DCCRailway.DCCController.Commands;
using DCCRailway.DCCLayout;
using DCCRailway.Utilities;

namespace DCCRailway.DCCController.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutGenericCmdUpdater(DCCRailwayConfig config)  {
    protected DCCRailwayConfig Config { get; } = config;

    public bool Process(ICommand command) {
        Logger.Log.Information($"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");
        return true;
    }
}