using DCCRailway.CmdStation.Commands.Types.Base;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCmd {
    public byte Value { get; set; }
}