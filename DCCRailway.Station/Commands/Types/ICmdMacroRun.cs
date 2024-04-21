using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdMacroRun : ICommand, ISystemCmd {
    public byte Macro { get; set; }
}