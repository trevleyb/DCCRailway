using DCCRailway.CmdStation.Commands.Types.Base;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdMacroRun : ICommand, ISystemCmd {
    public byte Macro { get; set; }
}