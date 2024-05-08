using DCCRailway.CmdStation.Actions.Commands.Base;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdLocoSetFunction : ICommand, ILocoCmd {
    public byte Function { get; set; }
    public bool State    { get; set; }
}