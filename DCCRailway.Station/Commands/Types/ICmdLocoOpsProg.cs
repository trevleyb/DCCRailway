using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCmd {
    public byte Value { get; set; }
}