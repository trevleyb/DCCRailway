using DCCRailway.CmdStation.Actions.Commands.Base;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdAccyOpsProg : ICommand, IAccyCmd {
    public byte Value { get; set; }
}