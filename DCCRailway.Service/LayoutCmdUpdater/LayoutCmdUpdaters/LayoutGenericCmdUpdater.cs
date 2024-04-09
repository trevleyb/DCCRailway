using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;

namespace DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutGenericCmdUpdater(IDCCRailwayConfig config) {
    protected IDCCRailwayConfig Config { get; } = config;

    public bool Process(ICommand command) {
        Logger.Log.Information($"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");

        return true;
    }
}