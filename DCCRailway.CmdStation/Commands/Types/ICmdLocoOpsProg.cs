using DCCRailway.CmdStation.Commands.Types.Base;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCmd {
    public byte Value { get; set; }
}