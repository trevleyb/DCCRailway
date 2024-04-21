using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdLocoSetFunction : ICommand, ILocoCmd {
    public byte Function { get; set; }
    public bool State    { get; set; }
}