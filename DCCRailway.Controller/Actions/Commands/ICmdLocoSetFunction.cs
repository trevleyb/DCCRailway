using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdLocoSetFunction : ICommand, ILocoCmd {
    public byte Function { get; set; }
    public bool State    { get; set; }
}