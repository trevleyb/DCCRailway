using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdAccyOpsProg : ICommand, IAccyCmd {
    public byte Value { get; set; }
}