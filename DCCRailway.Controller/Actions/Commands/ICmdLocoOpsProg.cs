using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdLocoOpsProg : ICommand, ILocoCmd {
    public byte Value { get; set; }
}