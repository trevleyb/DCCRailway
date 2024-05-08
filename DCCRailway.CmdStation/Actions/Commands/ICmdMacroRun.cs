using DCCRailway.CmdStation.Actions.Commands.Base;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdMacroRun : ICommand, ISystemCmd {
    public byte Macro { get; set; }
}