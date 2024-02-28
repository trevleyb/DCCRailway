using DCCRailway.DCCController.Commands.Types.BaseTypes;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdMacroRun : ICommand, ISystemCmd {
    public byte Macro { get; set; }
}