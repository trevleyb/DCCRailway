using DCCRailway.CmdStation.Actions.Commands.Base;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdLocoOpsProg : ICommand, ILocoCmd {
    public byte Value { get; set; }
}