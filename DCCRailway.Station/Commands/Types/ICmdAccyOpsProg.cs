using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCmd {
    public byte Value { get; set; }
}