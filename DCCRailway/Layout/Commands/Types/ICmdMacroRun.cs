using DCCRailway.Layout.Commands.Types.BaseTypes;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdMacroRun : ICommand, ISystemCmd {
    public byte Macro { get; set; }
}