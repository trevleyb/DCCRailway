using DCCRailway.Configuration;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Utilities;

namespace DCCRailway.Layout.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutGenericCmdUpdater(DCCRailwayConfig config)  {
    protected DCCRailwayConfig Config { get; } = config;

    public bool Process(ICommand command) {
        Logger.Log.Information($"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");
        return true;
    }
}