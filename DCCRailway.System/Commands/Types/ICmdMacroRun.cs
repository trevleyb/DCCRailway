using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

public interface ICmdMacroRun : ICommand, ISystemCmd {
    public byte Macro { get; set; }
}