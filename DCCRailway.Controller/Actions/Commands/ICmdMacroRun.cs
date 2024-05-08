using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdMacroRun : ICommand, ISystemCmd {
    public byte Macro { get; set; }
}